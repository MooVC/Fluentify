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
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(nameof(subject));
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
        actual.ShouldNotBeSameAs(original);
        actual.Description.ShouldBe(original.Description);
        actual.Simple.ShouldBe(expected);
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
        actual.ShouldNotBeSameAs(original);
        actual.Description.ShouldBe(original.Description);
        actual.Simple.ShouldBe(expected);
    }
}