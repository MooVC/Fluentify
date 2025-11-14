namespace Fluentify;

using Microsoft.CodeAnalysis;

/// <summary>
/// Generates the SkipAutoInstantiation attribute, used to denote that a property of a record type should not be
/// automatically instantiated when applying <see cref="RecordGenerator"/>.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class SkipAutoInstantiationAttributeGenerator
    : AttributeGenerator
{
    /// <summary>
    /// The name of the SkipAutoInstantiation attribute.
    /// </summary>
    internal const string Name = "SkipAutoInstantiation";

    /// <summary>
    /// Creates an instance of the <see cref="SkipAutoInstantiationAttributeGenerator"/>.
    /// </summary>
    public SkipAutoInstantiationAttributeGenerator()
        : base(Name, "Parameter", "Property", "Class", "Struct")
    {
    }
}