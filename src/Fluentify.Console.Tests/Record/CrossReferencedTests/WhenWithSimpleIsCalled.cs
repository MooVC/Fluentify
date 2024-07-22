namespace Fluentify.Console.Record.CrossReferencedTests;

public sealed class WhenWithSimpleIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        CrossReferenced? subject = default;

        // Act
        Func<CrossReferenced> act = () => subject!.WithSimple(new Simple());

        // Assert
        _ = act.Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(subject));
    }

    [Fact]
    public void GivenAnInstanceThenTheValueIsApplied()
    {
        // Arrange
        var original = new CrossReferenced
        {
            Description = "Cross Referenced",
            Simple = new(),
        };

        var expected = new Simple();

        // Act
        CrossReferenced actual = original.WithSimple(expected);

        // Assert
        _ = actual.Should().NotBeSameAs(original);
        _ = actual.Description.Should().Be(original.Description);
        _ = actual.Simple.Should().Be(expected);
    }

    [Fact]
    public void GivenABuilderThenTheValueIsApplied()
    {
        // Arrange
        var original = new CrossReferenced
        {
            Description = "Cross Referenced",
            Simple = new(),
        };

        var expected = new Simple();

        // Act
        CrossReferenced actual = original.WithSimple(_ => expected);

        // Assert
        _ = actual.Should().NotBeSameAs(original);
        _ = actual.Description.Should().Be(original.Description);
        _ = actual.Simple.Should().Be(expected);
    }
}