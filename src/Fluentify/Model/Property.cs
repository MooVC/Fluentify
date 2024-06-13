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
    /// Gets or sets the property kind, based on the type defined within the subject.
    /// </summary>
    /// <value>
    /// The property kind, based on the type defined within the subject.
    /// </value>
    public Kind Kind { get; set; } = Kind.Unspecified;

    /// <summary>
    /// Gets a value indicating whether or not the property has been annotated with the Ignore attribute.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the property has been annotated with the Ignore attribute.
    /// </value>
    public bool IsIgnored => Kind == Kind.Unspecified;

    /// <summary>
    /// Gets or sets the name of the property as defined within the subject.
    /// </summary>
    /// <value>
    /// The name of the property as defined within the subject.
    /// </value>
    public string Name { get; set; } = string.Empty;

    /// <inheritdoc/>
    protected override IEnumerable<object> GetProperties()
    {
        yield return Accessibility;
        yield return Descriptor;
        yield return Kind;
        yield return Name;
    }
}