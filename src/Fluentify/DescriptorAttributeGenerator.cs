namespace Fluentify;

using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

/// <summary>
/// Generates the Descriptor attribute, used to denote the preferred name for the extension methods associated with
/// a property of a record type is to be subjected to <see cref="FluentifyGenerator"/>.
/// </summary>
[Generator]
public sealed class DescriptorAttributeGenerator
    : IIncrementalGenerator
{
    /// <summary>
    /// The name of the attribute that will be output by the generator.
    /// </summary>
    public const string Name = "Descriptor";

    /// <summary>
    /// The source code that will be output by the generator.
    /// </summary>
    public const string Source = $$"""
            namespace Fluentify;

            using System;
            using System.Diagnostics.CodeAnalysis;

            [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
            internal sealed class {{Name}}Attribute
                : Attribute
            {
                public {{Name}}Attribute(string value)
                {
                    Value = value;
                }

                public string Value { get; }
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