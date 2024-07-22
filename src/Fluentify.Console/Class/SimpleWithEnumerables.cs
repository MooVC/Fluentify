﻿namespace Fluentify.Console.Class;

/// <summary>
/// A class that demonstrates the libraries use without generics.
/// </summary>
[Fluentify]
internal sealed class SimpleWithEnumerables
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
    public required IEnumerable<object> Attributes { get; init; }

    /// <summary>
    /// Gets or sets the fourth property to be subject to the extension generator.
    /// </summary>
    /// <value>
    /// The fourth property to be subject to the extension generator.
    /// </value>
    public required IReadOnlyCollection<int> Numbers { get; init; }

    /// <summary>
    /// Gets or sets the fifth property to be subject to the extension generator.
    /// </summary>
    /// <value>
    /// The fifth property to be subject to the extension generator.
    /// </value>
    public required IReadOnlyList<string> Names { get; init; }
}