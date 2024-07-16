namespace Fluentify.Console.Record.Example;

/// <summary>
/// A recreation of the example provided in the README.md.
/// </summary>
/// <param name="Birthday">The year in which the Actor was born.</param>
/// <param name="FirstName">The given name for the Actor.</param>
/// <param name="Surname">The family name for the Actor.</param>
[Fluentify]
public partial record Actor(
    [Descriptor("BornIn")] int Birthday,
    string FirstName,
    string Surname);