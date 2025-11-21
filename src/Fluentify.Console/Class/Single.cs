namespace Fluentify.Console.Class;

/// <summary>
/// A class that demonstrates the libraries use on a type with just one property.
/// </summary>
[Fluentify]
internal sealed class Single
{
    /// <summary>
    /// Gets the first property to be subject to the extension generator.
    /// </summary>
    /// <value>
    /// The first property to be subject to the extension generator.
    /// </value>
    public int Age { get; init; }
}