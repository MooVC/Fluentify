namespace Fluentify.Console.Class.MultipleGenericsTests;

public sealed class WhenWithAgeIsCalled
{
    [Test]
    [Arguments(1)]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(int.MinValue)]
    [Arguments(int.MaxValue)]
    public void GivenAnAgeThenTheValueIsApplied(int age)
    {
        // Arrange
        var original = new MultipleGenerics<int, object, List<string>>
        {
            Age = Random.Shared.Next(),
            Attributes = ["Captain", "USS Defiant"],
            Name = "Avery Brooks",
        };

        // Act
        MultipleGenerics<int, object, List<string>> actual = original.WithAge(age);

        // Assert
        actual.ShouldNotBeSameAs(original);
        actual.Age.ShouldBe(age);
        actual.Attributes.ShouldBeEquivalentTo(original.Attributes);
        actual.Name.ShouldBeEquivalentTo(original.Name);
    }

    [Test]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        MultipleGenerics<int, object, List<string>>? subject = default;

        // Act
        Func<MultipleGenerics<int, object, List<string>>> act = () => subject!.WithAge(1);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(nameof(subject));
    }
}