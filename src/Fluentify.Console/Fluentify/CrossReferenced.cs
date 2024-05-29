namespace Fluentify.Console.Fluentify;

/// <summary>
/// A record that demonstrates the libraries use with a cross referenced type that is also subject to Fluentify.
/// </summary>
/// <param name="Description">The first property to be subject to the extension generator.</param>
/// <param name="Simple">The second property to be subject to the extension generator.</param>
[Fluentify]
public sealed record CrossReferenced(string Description, Simple Simple);