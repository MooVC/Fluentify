namespace Fluentify.Console.Class;

/// <summary>
/// A class that demonstrates the libraries use without generics.
/// </summary>
[Fluentify]
internal sealed class SimpleWithCollection
{
    public required int Age { get; init; }

    public required string Name { get; init; } = string.Empty;

    public required List<object> Attributes { get; init; }
}