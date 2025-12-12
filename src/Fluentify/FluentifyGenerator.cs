namespace Fluentify;

using System.Text;
using Fluentify.Model;
using Fluentify.Source;
using Fluentify.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

/// <summary>
/// Generates extension methods that engineers to rapidly develop rich, expressive, and maintainable Fluent APIs with ease.
/// </summary>
/// <typeparam name="T">The type of syntax to which the generator applies.</typeparam>
public abstract partial class FluentifyGenerator<T>
    : IIncrementalGenerator
    where T : TypeDeclarationSyntax
{
    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<T?> records = context
            .SyntaxProvider
            .CreateSyntaxProvider(predicate: IsMatch, transform: Transform)
            .Where(record => record is not null);

        IncrementalValuesProvider<Subject?> subjects = records
           .Combine(context.CompilationProvider)
           .Select(static (match, cancellationToken) => match.Left.ToSubject(match.Right, cancellationToken))
           .Where(subject => subject is not null);

        context.RegisterSourceOutput(subjects, Generate);
    }

    /// <summary>
    /// Generates a unique hintname based on the property and subject combination.
    /// </summary>
    /// <param name="subject">The subject for which the context is generated.</param>
    /// <param name="property">The property for which the context was generated.</param>
    /// <param name="suffix">An optional suffix for the hint.</param>
    /// <returns>The unique hint name.</returns>
    private protected static string GetHint(Subject subject, Property? property = default, string? suffix = default)
    {
        string name = subject.Name;

        if (subject.Nesting.Count > 0)
        {
            IEnumerable<string> names = subject.Nesting
                .Reverse()
                .Select(parent => parent.Name)
                .Union([name]);

            name = string.Join(".", names);
        }

        if (property is not null)
        {
            name = $"{name}Extensions.{property.Descriptor}";
        }

        if (suffix is not null)
        {
            name = $"{name}.{suffix}";
        }

        return $"{name}.g.cs";
    }

    /// <summary>
    /// Allows for the customization of the transfrm generated for the specified property.
    /// </summary>
    /// <param name="property">The property for which the transform is to be generated.</param>
    /// <param name="subject">The subject for which the transform is being applied.</param>
    /// <returns>The source code associated with the property transform.</returns>
    private protected abstract string? GetScalarExtensionMethodBody(Property property, Subject subject);

    /// <summary>
    /// Allows for the customization of source to be added to the <see cref="SourceProductionContext"/>.
    /// </summary>
    /// <param name="subject">The subject for which source is to be added to the <see cref="SourceProductionContext"/>.</param>
    /// <returns>The source to be added to the <see cref="SourceProductionContext"/>.</returns>
    private protected virtual IEnumerable<Source> GetSource(Subject subject)
    {
        var metadata = subject.ToMetadata();

        foreach (Property property in subject.Properties
            .Where(property => !property.IsIgnored)
            .OrderBy(property => property.Name))
        {
            string? GetScalar(Property property)
            {
                return GetScalarExtensionMethodBody(property, subject);
            }

            string content = property.GetExtensions(ref metadata, GetScalar);

            if (!string.IsNullOrEmpty(content))
            {
                string hint = GetHint(subject, property: property);

                yield return new Source
                {
                    Content = content,
                    Hint = hint,
                };
            }
        }
    }

    /// <summary>
    /// Applies contextual specific content, typically preprocessor directives, to the <paramref name="content"/>.
    /// </summary>
    /// <param name="content">The content to which the additional content is to be applied.</param>
    /// <returns>The <paramref name="content"/> with the additional content applied.</returns>
    private protected abstract string Wrap(string content);

    private static bool IsMatch(SyntaxNode node, CancellationToken cancellationToken)
    {
        return node is T type && type.AttributeLists.Count > 0;
    }

    private static T? Transform(GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        return context.Node as T;
    }

    private void AddSource(string content, SourceProductionContext context, string hint, string @namespace)
    {
        if (!string.IsNullOrEmpty(@namespace))
        {
            content = $$"""
                namespace {{@namespace}}
                {
                    {{content.Indent()}}
                }
                """;

            hint = $"{@namespace}.{hint}";
        }

        content = $"""
            #pragma warning disable CS8625

            {content}
            
            #pragma warning restore CS8625
            """;

        content = Wrap(content);

        var text = SourceText.From(content, Encoding.UTF8);

        context.AddSource(hint, text);
    }

    private void Generate(SourceProductionContext context, Subject? subject)
    {
        if (subject is not null)
        {
            IEnumerable<Source> source = GetSource(subject);

            foreach (Source element in source)
            {
                AddSource(element.Content, context, element.Hint, subject.Namespace);
            }
        }
    }
}