namespace Fluentify;

using Microsoft.CodeAnalysis;

/// <summary>
/// Generates the AutoInitiateWith attribute, used to denote the static member to call when automatically instantiating a type.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class AutoInitiateWithAttributeGenerator
    : AttributeGenerator
{
    /// <summary>
    /// The name of the AutoInitiateWith attribute.
    /// </summary>
    internal const string Name = "AutoInitiateWith";

    /// <summary>
    /// Creates an instance of the <see cref="AutoInitiateWithAttributeGenerator"/>.
    /// </summary>
    public AutoInitiateWithAttributeGenerator()
        : base(Name, "Class", "Parameter", "Property", "Struct")
    {
    }
}
