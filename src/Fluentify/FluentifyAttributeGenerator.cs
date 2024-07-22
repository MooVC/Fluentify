namespace Fluentify;

using Microsoft.CodeAnalysis;

/// <summary>
/// Generates the Fluentify attribute, used to denote that a record type is to be subjected to <see cref="RecordGenerator"/>.
/// </summary>
[Generator]
public sealed class FluentifyAttributeGenerator
    : AttributeGenerator
{
    /// <summary>
    /// The name of the Fluentify attribute.
    /// </summary>
    internal const string Name = "Fluentify";

    /// <summary>
    /// Creates an instance of the <see cref="FluentifyAttributeGenerator"/>.
    /// </summary>
    public FluentifyAttributeGenerator()
        : base(Name, "Class")
    {
    }
}