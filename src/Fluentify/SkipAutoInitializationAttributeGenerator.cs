namespace Fluentify;

using Microsoft.CodeAnalysis;

/// <summary>
/// Generates the SkipAutoInitialization attribute, used to denote that a property or type cannot be automatically instantiated.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class SkipAutoInitializationAttributeGenerator
    : AttributeGenerator
{
    /// <summary>
    /// The name of the SkipAutoInitialization attribute.
    /// </summary>
    internal const string Name = "SkipAutoInitialization";

    /// <summary>
    /// Creates an instance of the <see cref="SkipAutoInitializationAttributeGenerator"/>.
    /// </summary>
    public SkipAutoInitializationAttributeGenerator()
        : base(Name, "Class", "Parameter", "Property", "Struct")
    {
    }
}