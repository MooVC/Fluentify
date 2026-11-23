namespace Fluentify;

using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using MooVC.Syntax;
using MooVC.Syntax.CSharp;

/// <summary>
/// Generates an extension method that checks if the supplied value is <see langword="null" />, thereby enabling support for all versions from .NET Standard 2.0 and above.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class InternalExtensionsGenerator
    : IIncrementalGenerator
{
    /// <summary>
    /// The source code that will be output by the generator.
    /// </summary>
    internal static readonly string Source = CreateSource();

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(Generate);
    }

    private static string CreateSource()
    {
        const string NullCheck = """
            if (subject == null)
            {
                throw new global::System.ArgumentNullException(paramName);
            }
            """;

        Symbol subject = (Qualification)"T";
        Declaration declaration = ((Declaration)"ThrowIfNull").WithArguments(Generic.Undefined.Named("T"));
        Method method = Method.Undefined
            .Named(declaration)
            .WithExtensibility(Modifiers.Static)
            .Accepts(parameter => parameter.Named("Subject").OfType(subject).WithModifier(Parameter.Modes.This))
            .Accepts(parameter => parameter.Named("ParamName").OfType(typeof(string)))
            .Returns(Result.Void)
            .WithBody(Snippet.From(Configuration.Options, NullCheck));

        return Definition.Undefined
            .From("Fluentify.Internal")
            .For<Class>(extension => extension
                .Named("Extensions")
                .WithScope(Scopes.Internal)
                .IsPartial(false)
                .IsStatic(true)
                .WithMethods(method))
            .ToSnippet(Configuration.Options);
    }

    private static void Generate(IncrementalGeneratorPostInitializationContext context)
    {
        var text = SourceText.From(Source, Encoding.UTF8);

        context.AddSource("Fluentify.Internal.Extensions.g.cs", text);
    }
}