namespace Fluentify.Console.Record.Example;

using Fluentify.Console.Record.Example.Simple;

/// <summary>
/// A recreation of the example provided in the README.md.
/// </summary>
/// <param name="Actors">A collection of Actors who starred in the Movie.</param>
/// <param name="Genre">The <see cref="Genre"/> of the Movie.</param>
/// <param name="ReleasedOn">The date on which the Movie was released.</param>
/// <param name="Title">The title of the Movie.</param>
[Fluentify]
public partial record Movie(Actor[] Actors, [Descriptor("OfGenre")] Genre Genre, [Descriptor] DateOnly ReleasedOn, string Title);