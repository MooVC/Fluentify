namespace Fluentify.Console.Record;

/// <summary>
/// A record that demonstrates the libraries use without generics.
/// </summary>
/// <param name="Age">The first property to be subject to the extension generator.</param>
/// <param name="Name">The second property to be subject to the extension generator.</param>
/// <param name="Attributes">The third property to be subject to the extension generator.</param>
/// <param name="Numbers">The fourth property to be subject to the extension generator.</param>
/// <param name="Names">The fifth property to be subject to the extension generator.</param>
[Fluentify]
internal sealed partial record SimpleWithEnumerables(
    int Age,
    string Name,
    IEnumerable<object>? Attributes = default,
    IReadOnlyCollection<int>? Numbers = default,
    IReadOnlyList<string>? Names = default);