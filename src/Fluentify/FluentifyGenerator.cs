namespace Fluentify;

using System.Text;
using Fluentify.Model;
using Fluentify.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using MooVC.Syntax;

/// <summary>
/// Generates extension methods that engineers to rapidly develop rich, expressive, and maintainable Fluent APIs with ease.
/// </summary>
/// <typeparam name="T">The type of syntax to which the generator applies.</typeparam>
public abstract class FluentifyGenerator<T>
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

        IncrementalValueProvider<bool> supportsNullableReferenceTypes = context.CompilationProvider
            .Select(static (compilation, _) => IsNullableReferenceTypesSupported(compilation));

        IncrementalValuesProvider<Subject?> subjects = records
           .Combine(context.CompilationProvider)
           .Combine(supportsNullableReferenceTypes)
           .Select(static (match, cancellationToken) => match.Left.Left.ToSubject(match.Left.Right, match.Right, cancellationToken))
           .Where(subject => subject is not null);

        context.RegisterSourceOutput(subjects, Generate);
    }

    private static void AddSource(SourceProductionContext context, Source source, Subject subject)
    {
        Snippet content = source.Content;
        IEnumerable<string> names = subject.Nesting.Reverse().Select(parent => parent.Name).Append(source.Hint);

        if (!string.IsNullOrEmpty(subject.Namespace))
        {
            content = content.Block(Configuration.Options, $"namespace {subject.Namespace}");
            names = names.Prepend(subject.Namespace);
        }

        if (subject.SupportsNullableReferenceTypes)
        {
            content = Wrap(content);
        }

        string hint = string.Join(".", names);
        var text = SourceText.From(content.ToString().Replace("Attribute/**/", "Attribute"), Encoding.UTF8);

        context.AddSource($"{hint}.g.cs", text);
    }

    private static bool IsMatch(SyntaxNode node, CancellationToken cancellationToken)
    {
        return node is T type && type.AttributeLists.Count > 0;
    }

    private static T? Transform(GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        return context.Node as T;
    }

    private static bool IsNullableReferenceTypesSupported(Compilation compilation)
    {
        SyntaxTree syntax = compilation.SyntaxTrees.FirstOrDefault();

        if (syntax is null)
        {
            return false;
        }

        return syntax.Options is CSharpParseOptions options && options.LanguageVersion >= LanguageVersion.CSharp8;
    }

    private static Snippet Wrap(Snippet content)
    {
        Snippet start = Snippet.From(Configuration.Options, "#nullable enable", "#pragma warning disable CS8625");
        Snippet end = Snippet.From(Configuration.Options, "#pragma warning restore CS8625", "#nullable restore");

        return Snippet.Blank.Combine(Configuration.Options, start, content, end);
    }

    private static void Generate(SourceProductionContext context, Subject? subject)
    {
        if (subject is null)
        {
            return;
        }

        var navigator = new SubjectNavigator(GenerationServices.Instance);

        foreach (Source source in navigator.Navigate<Source>(subject))
        {
            AddSource(context, source, subject);
        }
    }
}