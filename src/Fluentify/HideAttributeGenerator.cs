namespace Fluentify;

using Microsoft.CodeAnalysis;

/// <summary>
/// Generates the Hide attribute, used to denote that a property of a record type should expose internal extension methods.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class HideAttributeGenerator
    : AttributeGenerator
{
    /// <summary>
    /// The name of the Hide attribute.
    /// </summary>
    internal const string Name = "Hide";

    /// <summary>
    /// Creates an instance of the <see cref="HideAttributeGenerator"/>.
    /// </summary>
    public HideAttributeGenerator()
        : base(Name, "Parameter", "Property")
    {
    }
}