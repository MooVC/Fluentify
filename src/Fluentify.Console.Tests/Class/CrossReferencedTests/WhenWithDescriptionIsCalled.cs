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
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(nameof(subject));
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
        actual.ShouldNotBeSameAs(original);
        actual.Description.ShouldBe(description);
        actual.Simple.ShouldBe(original.Simple);
    }
}