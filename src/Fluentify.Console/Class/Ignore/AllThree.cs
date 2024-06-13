namespace Fluentify.Console.Class.Ignore;

using System.Collections.Generic;

/// <summary>
/// A class that demonstrates the libraries use without generics, with all properties ignored.
/// </summary>
[Fluentify]
internal sealed class AllThree
{
    [Ignore]
    public required int Age { get; init; }

    [Ignore]
    public required string Name { get; init; }

    [Ignore]
    public required IReadOnlyList<object>? Attributes { get; init; }
}