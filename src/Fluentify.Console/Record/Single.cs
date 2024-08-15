namespace Fluentify.Console.Record;

/// <summary>
/// A record that demonstrates the libraries use on a type with just one property.
/// </summary>
/// <param name="Age">The first property to be subject to the extension generator.</param>
[Fluentify]
internal sealed partial record Single(int Age);