namespace Fluentify;

using Microsoft.CodeAnalysis;

/// <summary>
/// Generates the Ignore attribute, used to denote that a property of a record type is not to be subjected to <see cref="FluentifyGenerator"/>.
/// </summary>
[Generator]
public sealed class IgnoreAttributeGenerator
    : AttributeGenerator
{
    /// <summary>
    /// The name of the Ignore attribute.
    /// </summary>
    public const string Name = "Ignore";

    /// <summary>
    /// Creates an instance of the <see cref="IgnoreAttributeGenerator"/>.
    /// </summary>
    public IgnoreAttributeGenerator()
        : base(Name, "Parameter", "Property")
    {
    }
}