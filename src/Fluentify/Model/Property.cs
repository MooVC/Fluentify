namespace Fluentify.Model;

using Microsoft.CodeAnalysis;
using Valuify;

/// <summary>
/// The definition of the <see cref="Property"/> type, which is used to capture information relating to a property
/// that is to be supported through a fluent extension.
/// </summary>
[Valuify]
internal sealed partial class Property
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
    /// Gets or sets a value indicating whether or not the property has been annotated with the Ignore attribute.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the property has been annotated with the Ignore attribute.
    /// </value>
    public bool IsIgnored { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the property has been annotated with the Hide attribute.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the property has been annotated with the Hide attribute.
    /// </value>
    public bool IsHidden { get; set; }

    /// <summary>
    /// Gets or sets the name of the property as defined within the subject.
    /// </summary>
    /// <value>
    /// The name of the property as defined within the subject.
    /// </value>
    public string Name { get; set; } = string.Empty;
}