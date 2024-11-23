namespace Fluentify.Model;

using Valuify;

/// <summary>
/// The definition of the <see cref="Generic"/> type, which is used to capture information relating to a generic parameter.
/// </summary>
[Valuify]
internal sealed partial class Generic
{
    /// <summary>
    /// Gets or sets the constraints associated with the generic type parameter.
    /// </summary>
    /// <value>
    /// The constraints associated with the generic type parameter.
    /// </value>
    public IReadOnlyList<string> Constraints { get; set; } = [];

    /// <summary>
    /// Gets or sets the name of the property type defined within the subject.
    /// </summary>
    /// <value>
    /// The name of the property type defined within the subject.
    /// </value>
    public string Name { get; set; } = string.Empty;
}