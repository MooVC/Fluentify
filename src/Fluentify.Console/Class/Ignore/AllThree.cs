namespace Fluentify.Console.Class.Ignore;

using System.Collections.Generic;

/// <summary>
/// A class that demonstrates the libraries use without generics, with all properties ignored.
/// </summary>
[Fluentify]
internal sealed class AllThree
{
    /// <summary>
    /// Gets or sets the first property to be subject to the extension generator.
    /// </summary>
    /// <value>
    /// The first property to be subject to the extension generator.
    /// </value>
    [Ignore]
    public required int Age { get; init; }

    /// <summary>
    /// Gets or sets the second property to be subject to the extension generator.
    /// </summary>
    /// <value>
    /// The second property to be subject to the extension generator.
    /// </value>
    [Ignore]
    public required string Name { get; init; }

    /// <summary>
    /// Gets or sets the third property to be subject to the extension generator.
    /// </summary>
    /// <value>
    /// The third property to be subject to the extension generator.
    /// </value>
    [Ignore]
    public required IReadOnlyList<object>? Attributes { get; init; }
}