namespace Fluentify.Console.Record.SelfDescriptor.OnRequiredTests;

public sealed class WhenAgeIsCalled
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
        var original = new OnRequired
        {
            Age = Random.Shared.Next(),
            Attributes = [new(), new()],
            Name = "Avery Brooks",
        };

        // Act
        OnRequired actual = original.Age(age);

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
        OnRequired? subject = default;

        // Act
        Func<OnRequired> act = () => subject!.Age(1);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(nameof(subject));
    }
}