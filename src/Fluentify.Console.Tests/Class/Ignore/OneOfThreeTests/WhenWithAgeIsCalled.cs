namespace Fluentify.Console.Class.Ignore.OneOfThreeTests;

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
        var original = new OneOfThree
        {
            Age = Random.Shared.Next(),
            Attributes = [new(), new()],
            Name = "Avery Brooks",
        };

        // Act
        OneOfThree actual = original.WithAge(age);

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
        OneOfThree? subject = default;

        // Act
        Func<OneOfThree> act = () => subject!.WithAge(1);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(nameof(subject));
    }
}