namespace Fluentify.Console.Class;

/// <summary>
/// A class that demonstrates the libraries use without generics.
/// </summary>
[Fluentify]
internal sealed class Simple
{
    public int Age { get; init; }

    public string Name { get; init; } = string.Empty;

    public IReadOnlyList<object>? Attributes { get; init; }
}