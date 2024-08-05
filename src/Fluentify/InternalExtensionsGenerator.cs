namespace Fluentify;

using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

/// <summary>
/// Generates an extension method that checks if the supplied value is null, thereby enabling support for all versions from .NET Standard 2.0 and above.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class InternalExtensionsGenerator
    : IIncrementalGenerator
{
    /// <summary>
    /// The source code that will be output by the generator.
    /// </summary>
    internal const string Source = $$"""
        namespace Fluentify.Internal
        {
            using System;

            internal static class Extensions
            {
                public static void ThrowIfNull(this object subject, string paramName)
                {
                    if (subject == null)
                    {
                        throw new ArgumentNullException(paramName);
                    }
                }
            }
        }
        """;

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(Generate);
    }

    private static void Generate(IncrementalGeneratorPostInitializationContext context)
    {
        var text = SourceText.From(Source, Encoding.UTF8);

        context.AddSource($"Fluentify.Internal.Extensions.g.cs", text);
    }
}