namespace Fluentify;

using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

/// <summary>
/// Generates the Builder delegate, used to facilitate onward configurability within the fluent chain.
/// </summary>
[Generator]
public sealed class BuilderDelegateGenerator
    : IIncrementalGenerator
{
    /// <summary>
    /// The source code that will be output by the generator.
    /// </summary>
    public const string Source = $$"""
        namespace Fluentify;

        internal delegate T Builder<T>(T subject)
            where T : new();
        """;

    private const string Hint = "Builder.g.cs";

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(Generate);
    }

    private static void Generate(IncrementalGeneratorPostInitializationContext context)
    {
        var text = SourceText.From(Source, Encoding.UTF8);

        context.AddSource(Hint, text);
    }
}