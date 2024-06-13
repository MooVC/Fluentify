namespace Fluentify.Console.Class.Descriptor;

using System.Collections.Generic;

/// <summary>
/// A class that demonstrates the libraries use without generics.
/// </summary>
[Fluentify]
internal sealed class OnOptional
{
    public required int Age { get; init; }

    public required string Name { get; init; }

    [Descriptor("AttributedWith")]
    public required IReadOnlyList<object>? Attributes { get; init; }
}