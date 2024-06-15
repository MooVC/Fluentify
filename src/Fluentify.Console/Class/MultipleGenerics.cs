namespace Fluentify.Console.Class;

using System.Collections.Generic;

/// <summary>
/// A class that demonstrates the libraries use with multiple generics.
/// </summary>
/// <typeparam name="T1">The first type parameter.</typeparam>
/// <typeparam name="T2">The second type parameter.</typeparam>
/// <typeparam name="T3">The third type parameter.</typeparam>
[Fluentify]
internal sealed class MultipleGenerics<T1, T2, T3>
    where T1 : struct
    where T2 : class, new()
    where T3 : IEnumerable<string>
{
    /// <summary>
    /// Gets or sets the first property to be subject to the extension generator.
    /// </summary>
    /// <value>
    /// The first property to be subject to the extension generator.
    /// </value>
    public required T1? Age { get; init; }

    /// <summary>
    /// Gets or sets the second property to be subject to the extension generator.
    /// </summary>
    /// <value>
    /// The second property to be subject to the extension generator.
    /// </value>
    public required T2? Name { get; init; }

    /// <summary>
    /// Gets or sets the third property to be subject to the extension generator.
    /// </summary>
    /// <value>
    /// The third property to be subject to the extension generator.
    /// </value>
    public required T3 Attributes { get; init; }
}