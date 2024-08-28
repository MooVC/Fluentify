namespace Fluentify.Console.Class.SelfDescriptor;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// A class that demonstrates the libraries use without generics.
/// </summary>
[Fluentify]
internal sealed class OnIgnored
{
    /// <summary>
    /// Gets the first property to be subject to the extension generator.
    /// </summary>
    /// <value>
    /// The first property to be subject to the extension generator.
    /// </value>
    public required int Age { get; init; }

    /// <summary>
    /// Gets the second property to be subject to the extension generator.
    /// </summary>
    /// <value>
    /// The second property to be subject to the extension generator.
    /// </value>
    [Descriptor]
    [Ignore]
    [SuppressMessage("Usage", "FLTFY02:Descriptor is disregarded from consideration by Fluentify", Justification = "The test is intended to demonstrate the outcome of combining the two attributes.")]
    public required string Name { get; init; }

    /// <summary>
    /// Gets the third property to be subject to the extension generator.
    /// </summary>
    /// <value>
    /// The third property to be subject to the extension generator.
    /// </value>
    public required IReadOnlyList<object>? Attributes { get; init; }
}