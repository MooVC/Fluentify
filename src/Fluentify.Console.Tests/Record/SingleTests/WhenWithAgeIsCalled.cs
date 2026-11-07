namespace Fluentify.Console.Record.SingleTests;

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
        var original = new Single
        {
            Age = Random.Shared.Next(),
        };

        // Act
        Single actual = original.WithAge(age);

        // Assert
        actual.ShouldNotBeSameAs(original);
        actual.Age.ShouldBe(age);
    }

    [Test]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        Single? subject = default;

        // Act
        Func<Single> act = () => subject!.WithAge(1);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(nameof(subject));
    }
}