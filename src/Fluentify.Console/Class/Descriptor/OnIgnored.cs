namespace Fluentify.Console.Class.Descriptor;

using System.Collections.Generic;

/// <summary>
/// A class that demonstrates the libraries use without generics.
/// </summary>
[Fluentify]
internal sealed class OnIgnored
{
    public required int Age { get; init; }

    [Descriptor("Named")]
    [Ignore]
    public required string Name { get; init; }

    public required IReadOnlyList<object>? Attributes { get; init; }
}