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
           .Select((match, cancellationToken) => match.Left.ToSubject(match.Right, cancellationToken))
           .Where(subject => subject is not null);

        context.RegisterSourceOutput(subjects, Generate);
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

        foreach (Property property in subject.Properties.Where(property => !property.IsIgnored))
        {
            string? GetScalar(Property property)
            {
                return GetScalarExtensionMethodBody(property, subject);
            }

            string content = property.GetExtensions(ref metadata, GetScalar);
            string hint = $"{subject.Namespace}.{subject.Name}Extensions.{property.Descriptor}.g.cs";

            yield return new Source
            {
                Content = content,
                Hint = hint,
            };
        }
    }

    private static void AddSource(string content, SourceProductionContext context, string hint, string @namespace)
    {
        content = $$"""
            #nullable enable
            #pragma warning disable CS8625

            namespace {{@namespace}}
            {
                {{content.Indent()}}
            }
            
            #pragma warning restore CS8625
            #nullable restore
            """;

        var text = SourceText.From(content, Encoding.UTF8);

        context.AddSource(hint, text);
    }

    private static bool IsMatch(SyntaxNode node, CancellationToken cancellationToken)
    {
        return node is T type && type.AttributeLists.Count > 0;
    }

    private static T? Transform(GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        return context.Node as T;
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