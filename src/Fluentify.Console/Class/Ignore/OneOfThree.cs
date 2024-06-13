namespace Fluentify.Console.Class.Ignore;

using System.Collections.Generic;

/// <summary>
/// A class that demonstrates the libraries use without generics, with selective property ignoring.
/// </summary>
[Fluentify]
internal sealed class OneOfThree
{
    public required int Age { get; init; }

    [Ignore]
    public required string Name { get; init; }

    public required IReadOnlyList<object>? Attributes { get; init; }
}