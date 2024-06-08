namespace Fluentify.Model;

using System.Collections.Generic;
using Microsoft.CodeAnalysis;

/// <summary>
/// The definition of the <see cref="Property"/> type, which is used to capture information relating to a property
/// that is to be supported through a fluent extension.
/// </summary>
internal sealed class Property
    : Value<Property>
{
    /// <summary>
    /// Gets or sets the declared accessibility modifier for the property as defined within the subject.
    /// </summary>
    /// <value>
    /// The declared accessibility modifier for the property as defined within the subject.
    /// </value>
    public Accessibility Accessibility { get; set; } = Accessibility.Public;

    /// <summary>
    /// Gets or sets the descriptor to use for the extensions associated with the <see cref="Property"/>.
    /// </summary>
    /// <value>
    /// The descriptor to use for the extensions associated with the <see cref="Property"/>.
    /// </value>
    public string Descriptor { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether or not the type of the property adheres to the new() constraint.
    /// </summary>
    /// <value>
    /// The value indicating whether or not the type of the property adheres to the new() constraint.
    /// </value>
    public bool IsBuildable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the property is deemed to be nullable.
    /// </summary>
    /// <value>
    /// The value indicating whether or not the property is deemed to be nullable.
    /// </value>
    public bool IsNullable { get; set; }

    /// <summary>
    /// Gets or sets the name of the property as defined within the subject.
    /// </summary>
    /// <value>
    /// The name of the property as defined within the subject.
    /// </value>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the property type defined within the subject.
    /// </summary>
    /// <value>
    /// The name of the property type defined within the subject.
    /// </value>
    public string Type { get; set; } = string.Empty;

    /// <inheritdoc/>
    protected override IEnumerable<object> GetProperties()
    {
        yield return Accessibility;
        yield return Descriptor;
        yield return IsBuildable;
        yield return IsNullable;
        yield return Name;
        yield return Type;
    }
}