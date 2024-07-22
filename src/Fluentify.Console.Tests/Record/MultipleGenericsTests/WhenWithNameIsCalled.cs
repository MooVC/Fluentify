namespace Fluentify.Console.Record.MultipleGenericsTests;

public sealed class WhenWithNameIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        MultipleGenerics<int, object, List<string>>? subject = default;

        // Act
        Func<MultipleGenerics<int, object, List<string>>> act = () => subject!.WithName("Avery Brooks");

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
        var original = new MultipleGenerics<int, object, List<string>>
        {
            Age = Random.Shared.Next(),
            Attributes = ["Captain", "USS Enterprise"],
            Name = "Patrick Stewart",
        };

        // Act
        MultipleGenerics<int, object, List<string>> actual = original.WithName(name);

        // Assert
        _ = actual.Should().NotBeSameAs(original);
        _ = actual.Age.Should().Be(original.Age);
        _ = actual.Attributes.Should().BeEquivalentTo(original.Attributes);
        _ = actual.Name.Should().BeEquivalentTo(name);
    }
}