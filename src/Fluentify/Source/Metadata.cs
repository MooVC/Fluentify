namespace Fluentify.Source;

using Fluentify.Model;

/// <summary>
/// Captures metadata used to support source generation.
/// </summary>
internal struct Metadata
{
    /// <summary>
    /// Gets or sets a list of generic type constraints associated with the <see cref="Subject"/>, including the clause statement.
    /// </summary>
    /// <value>
    /// A list of generic type constraints associated with the <see cref="Subject"/>, including the clause statement.
    /// </value>
    public IReadOnlyList<string> Constraints { get; set; }

    /// <summary>
    /// Gets or sets the generic type parameters for the <see cref="Subject"/>.
    /// </summary>
    /// <value>
    /// The generic type parameters for the <see cref="Subject"/>.
    /// </value>
    public string Parameters { get; set; }

    /// <summary>
    /// Gets or sets the subject to which the metadata relates.
    /// </summary>
    /// <value>
    /// The subject to which the metadata relates.
    /// </value>
    public Subject Subject { get; set; }

    /// <summary>
    /// Gets or sets the type name for the <see cref="Subject"/>.
    /// </summary>
    /// <value>
    /// The type name for the <see cref="Subject"/>.
    /// </value>
    public string Type { get; set; }
}