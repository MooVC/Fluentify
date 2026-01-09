namespace Fluentify;

using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

/// <summary>
/// Generates the AutoInitializeWith attribute, used to denote the static member to call when automatically instantiating a type.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class AutoInitializeWithAttributeGenerator
    : IIncrementalGenerator
{
    /// <summary>
    /// The name of the attribute that will be output by the generator.
    /// </summary>
    internal const string Name = "AutoInitializeWith";

    /// <summary>
    /// The source code that will be output by the generator.
    /// </summary>
    internal const string Source = $$"""
        namespace Fluentify
        {
            using System;
            using System.Diagnostics.CodeAnalysis;

            [AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
            internal sealed class {{Name}}Attribute
                : Attribute
            {
                public {{Name}}Attribute()
                {
                }

                public {{Name}}Attribute(string factory)
                {
                    Factory = factory;
                }

                public string Factory { get; } = string.Empty;
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

        context.AddSource($"{Name}Attribute.g.cs", text);
    }
}