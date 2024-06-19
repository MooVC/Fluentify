namespace Fluentify.Console.Class.SimpleWithBooleanTests;

public sealed class WhenWithNameIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        SimpleWithBoolean? subject = default;

        // Act
        Func<SimpleWithBoolean> act = () => subject!.WithName("Avery Brooks");

        // Assert
        _ = act.Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(subject));
    }

    [Theory]
    [InlineData("Avery Brooks")]
    [InlineData("")]
    [InlineData(" ")]
    public void GivenANameThenTheValueIsApplied(string name)
    {
        // Arrange
        var original = new SimpleWithBoolean
        {
            Age = Random.Shared.Next(),
            IsRetired = true,
            Name = "Patrick Stewart",
        };

        // Act
        SimpleWithBoolean actual = original.WithName(name);

        // Assert
        _ = actual.Should().NotBeSameAs(original);
        _ = actual.Age.Should().Be(original.Age);
        _ = actual.IsRetired.Should().Be(original.IsRetired);
        _ = actual.Name.Should().BeEquivalentTo(name);
    }
}