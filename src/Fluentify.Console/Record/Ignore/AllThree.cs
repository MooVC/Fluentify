namespace Fluentify.Console.Record.Ignore;

using System.Collections.Generic;

/// <summary>
/// A record that demonstrates the libraries use without generics.
/// </summary>
/// <param name="Age">The first property to be subject to the extension generator.</param>
/// <param name="Name">The second property to be subject to the extension generator.</param>
/// <param name="Attributes">The third property to be subject to the extension generator.</param>
[Fluentify]
internal sealed partial record AllThree([Ignore] int Age, [Ignore] string Name, [Ignore] IReadOnlyList<object>? Attributes = default);