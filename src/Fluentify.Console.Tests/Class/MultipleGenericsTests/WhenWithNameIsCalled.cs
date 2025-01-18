namespace Fluentify.Console.Class.MultipleGenericsTests;

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
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(nameof(subject));
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
        actual.ShouldNotBeSameAs(original);
        actual.Age.ShouldBe(original.Age);
        actual.Attributes.ShouldBeEquivalentTo(original.Attributes);
        actual.Name.ShouldBeEquivalentTo(name);
    }
}