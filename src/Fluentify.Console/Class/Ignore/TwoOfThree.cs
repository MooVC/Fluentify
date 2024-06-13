namespace Fluentify.Console.Class.Ignore;

using System.Collections.Generic;

/// <summary>
/// A class that demonstrates the libraries use without generics, with selective ignoring of properties.
/// </summary>
[Fluentify]
internal sealed class TwoOfThree
{
    [Ignore]
    public required int Age { get; init; }

    [Ignore]
    public required string Name { get; init; }

    public required IReadOnlyList<object>? Attributes { get; init; }
}