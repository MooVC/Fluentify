namespace Fluentify.Console.Class;

using System.Collections;

/// <summary>
/// A class that demonstrates the libraries use with a single generic.
/// </summary>
/// <typeparam name="T">The generic type parameter.</typeparam>
[Fluentify]
internal sealed class SingleGeneric<T>
    where T : IEnumerable
{
    public required int Age { get; init; }

    public required string Name { get; init; } = string.Empty;

    public required T? Attributes { get; init; }
}