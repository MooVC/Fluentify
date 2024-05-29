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
[Generator(LanguageNames.CSharp)]
public sealed class FluentifyGenerator
    : IIncrementalGenerator
{
    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<RecordDeclarationSyntax?> records = context
            .SyntaxProvider
            .CreateSyntaxProvider(predicate: IsMatch, transform: Transform)
            .Where(record => record is not null);

        IncrementalValuesProvider<Subject?> subjects = records
           .Combine(context.CompilationProvider)
           .Select((match, cancellationToken) => match.Left.ToSubject(match.Right, cancellationToken))
           .Where(subject => subject is not null);

        context.RegisterSourceOutput(subjects, Generate);
    }

    private static void AddSource(SourceProductionContext context, string hint, string @namespace, string source)
    {
        source = $$"""
            #nullable enable
            #pragma warning disable CS8625

            namespace {{@namespace}}
            {
                {{source.Indent()}}
            }
            
            #pragma warning restore CS8625
            #nullable restore
            """;

        var text = SourceText.From(source, Encoding.UTF8);

        context.AddSource(hint, text);
    }

    private static void Generate(SourceProductionContext context, Subject? subject)
    {
        if (subject is not null)
        {
            if (subject.IsPartial && !subject.HasDefaultConstructor)
            {
                GenerateConstructor(context, subject);
            }

            GenerateExtensions(context, subject);
        }
    }

    private static void GenerateConstructor(SourceProductionContext context, Subject subject)
    {
        string source = subject.GetConstructor();
        string hint = $"{subject.Namespace}.{subject.Name}.ctor.g.cs";

        AddSource(context, hint, subject.Namespace, source);
    }

    private static void GenerateExtensions(SourceProductionContext context, Subject subject)
    {
        var metadata = subject.ToMetadata();

        foreach (Property property in subject.Properties)
        {
            string source = property.GetExtensions(ref metadata);
            string hint = $"{subject.Namespace}.{subject.Name}Extensions.{property.Descriptor}.g.cs";

            AddSource(context, hint, subject.Namespace, source);
        }
    }

    private static bool IsMatch(SyntaxNode node, CancellationToken cancellationToken)
    {
        return node is RecordDeclarationSyntax record && record.AttributeLists.Count > 0;
    }

    private static RecordDeclarationSyntax? Transform(GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        return context.Node as RecordDeclarationSyntax;
    }
}