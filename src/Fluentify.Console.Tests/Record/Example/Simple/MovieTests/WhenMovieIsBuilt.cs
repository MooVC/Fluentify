namespace Fluentify.Console.Record.Example.Simple.MovieTests;

public sealed class WhenMovieIsBuilt
{
    [Fact]
    public void GivenAMovieThenTheInstanceIsCreated()
    {
        // Arrange
        var original = new Movie();

        var expected = new Movie
        {
            Actors =
            [
                new Actor
                {
                    Birthday = 1940,
                    FirstName = "Patrick",
                    Surname = "Stewart",
                },
            ],
            Genre = Genre.SciFi,
            ReleasedOn = new DateOnly(1996, 12, 13),
            Title = "Star Trek: First Contact",
        };

        // Act
        Movie actual = original
            .OfGenre(Genre.SciFi)
            .WithTitle("Star Trek: First Contact")
            .ReleasedOn(new DateOnly(1996, 12, 13))
            .WithActors(actor => actor
                .WithFirstName("Patrick")
                .WithSurname("Stewart")
                .BornIn(1940));

        // Assert
        actual.ShouldNotBe(original);
        actual.ShouldBeEquivalentTo(expected);
    }
}