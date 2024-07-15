namespace Fluentify.Console.Record.Example;

[Fluentify]
public partial record Actor(
    [Descriptor("BornIn")] int Birthday,
    string FirstName,
    string Surname);