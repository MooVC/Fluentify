namespace Fluentify.Console.Class.CrossReferencedTests;

public sealed class WhenWithDescriptionIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        CrossReferenced? subject = default;

        // Act
        Func<CrossReferenced> act = () => subject!.WithDescription("Cross Referenced");

        // Assert
        _ = act.Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(subject));
    }

    [Theory]
    [InlineData("Cross Referenced")]
    [InlineData("")]
    [InlineData(" ")]
    public void GivenADescriptionThenTheValueIsApplied(string description)
    {
        // Arrange
        var original = new CrossReferenced
        {
            Description = "Cross Referenced",
            Simple = new(),
        };

        // Act
        CrossReferenced actual = original.WithDescription(description);

        // Assert
        _ = actual.Should().NotBeSameAs(original);
        _ = actual.Description.Should().Be(description);
        _ = actual.Simple.Should().Be(original.Simple);
    }
}