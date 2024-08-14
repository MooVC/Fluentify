namespace Fluentify.Console.Class;

/// <summary>
/// A class that demonstrates the libraries use with a cross-referenced type that is also subject to Fluentify.
/// </summary>
[Fluentify]
internal sealed class CrossReferenced
{
    /// <summary>
    /// Gets the first property to be subject to the extension generator.
    /// </summary>
    /// <value>
    /// The first property to be subject to the extension generator.
    /// </value>
    public required string Description { get; init; }

    /// <summary>
    /// Gets the second property to be subject to the extension generator.
    /// </summary>
    /// <value>
    /// The second property to be subject to the extension generator.
    /// </value>
    public required Simple Simple { get; init; }
}