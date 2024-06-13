namespace Fluentify.Console.Class;

/// <summary>
/// A class that demonstrates the libraries use with a cross-referenced type that is also subject to Fluentify.
/// </summary>
[Fluentify]
internal sealed class CrossReferenced
{
    public required string Description { get; init; }

    public required Simple Simple { get; init; }
}