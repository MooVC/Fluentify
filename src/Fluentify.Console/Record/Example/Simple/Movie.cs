namespace Fluentify.Console.Record.Example;

using Fluentify.Console.Record.Example.Simple;

[Fluentify]
public partial record Movie(
    Actor[] Actors,
    [Descriptor("OfGenre")] Genre Genre,
    [Descriptor("ReleasedOn")] DateOnly ReleasedOn,
    string Title);