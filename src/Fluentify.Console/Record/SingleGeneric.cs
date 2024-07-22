namespace Fluentify.Console.Record;

using System.Collections;

/// <summary>
/// A record that demonstrates the libraries use with a single generic.
/// </summary>
/// <typeparam name="T">The generic type parameter.</typeparam>
/// <param name="Age">The first property to be subject to the extension generator.</param>
/// <param name="Name">The second property to be subject to the extension generator.</param>
/// <param name="Attributes">The third property to be subject to the extension generator.</param>
[Fluentify]
internal sealed partial record SingleGeneric<T>(int Age, string Name, T? Attributes = default)
    where T : IEnumerable;