namespace Fluentify.Console.Class;

using System.Collections;

/// <summary>
/// A class that demonstrates the libraries use with a single generic.
/// </summary>
/// <typeparam name="T">The generic type parameter.</typeparam>
[Fluentify]
internal sealed class SingleGeneric<T>
    where T : IEnumerable
{
    /// <summary>
    /// Gets or sets the first property to be subject to the extension generator.
    /// </summary>
    /// <value>
    /// The first property to be subject to the extension generator.
    /// </value>
    public required int Age { get; init; }

    /// <summary>
    /// Gets or sets the second property to be subject to the extension generator.
    /// </summary>
    /// <value>
    /// The second property to be subject to the extension generator.
    /// </value>
    public required string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the third property to be subject to the extension generator.
    /// </summary>
    /// <value>
    /// The third property to be subject to the extension generator.
    /// </value>
    public required T? Attributes { get; init; }
}