namespace Fluentify;

using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

/// <summary>
/// Generates the Descriptor attribute, used to denote the preferred name for the extension methods associated with
/// a property of a record type is to be subjected to <see cref="RecordGenerator"/>.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class DescriptorAttributeGenerator
    : IIncrementalGenerator
{
    /// <summary>
    /// The name of the attribute that will be output by the generator.
    /// </summary>
    internal const string Name = "Descriptor";

    /// <summary>
    /// The source code that will be output by the generator.
    /// </summary>
    internal static readonly string Source = AttributeGenerator.CreateStringAttributeSource(
        Name,
        "Value",
        "value",
        AttributeTargets.Parameter,
        AttributeTargets.Property);

    /// <summary>
    /// The pattern applied to the value supplied to the Descriptor, ensuring the value specified is suitable for use as an extension method name.
    /// </summary>
    internal static readonly Regex Pattern = new("^[A-Z][a-zA-Z0-9]*$", RegexOptions.Compiled);

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