namespace Fluentify.Console.Record.Descriptor;

using System.Collections.Generic;

/// <summary>
/// A record that demonstrates the libraries use without generics.
/// </summary>
/// <param name="Age">The first property to be subject to the extension generator.</param>
/// <param name="Name">The second property to be subject to the extension generator.</param>
/// <param name="Attributes">The third property to be subject to the extension generator.</param>
[Fluentify]
internal sealed record OnIgnored(int Age, [Descriptor("Named"), Ignore] string Name, IReadOnlyList<object>? Attributes = default);