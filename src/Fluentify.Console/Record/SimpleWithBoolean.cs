namespace Fluentify.Console.Record;

/// <summary>
/// A record that demonstrates the libraries use without generics.
/// </summary>
/// <param name="Age">The first property to be subject to the extension generator.</param>
/// <param name="IsRetired">The second property to be subject to the extension generator.</param>
/// <param name="Name">The third property to be subject to the extension generator.</param>
[Fluentify]
internal sealed partial record SimpleWithBoolean(int Age, bool? IsRetired, string Name);