namespace Fluentify.Console.Class;

/// <summary>
/// A class that demonstrates the libraries use without generics.
/// </summary>
[Fluentify]
internal sealed class SimpleWithArray
{
    public required int Age { get; init; }

    public required string Name { get; init; } = string.Empty;

    public required object[]? Attributes { get; init; }
}