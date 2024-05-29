namespace Fluentify.Console.Fluentify;

/// <summary>
/// A record that demonstrates the libraries use with multiple generics.
/// </summary>
/// <typeparam name="T1">The first type parameter.</typeparam>
/// <typeparam name="T2">The second type parameter.</typeparam>
/// <typeparam name="T3">The third type parameter.</typeparam>
/// <param name="Age">The first property to be subject to the extension generator.</param>
/// <param name="Name">The second property to be subject to the extension generator.</param>
/// <param name="Attributes">The third property to be subject to the extension generator.</param>
[Fluentify]
internal sealed record MultipleGenerics<T1, T2, T3>(T1? Age, T2? Name, T3 Attributes)
    where T1 : struct
    where T2 : class, new()
    where T3 : IEnumerable<string>;