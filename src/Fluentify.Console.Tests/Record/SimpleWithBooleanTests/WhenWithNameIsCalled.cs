namespace Fluentify.Console.Record.SimpleWithBooleanTests;

public sealed class WhenWithNameIsCalled
{
    [Test]
    [Arguments("Avery Brooks")]
    [Arguments("")]
    [Arguments(" ")]
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
        actual.ShouldNotBeSameAs(original);
        actual.Age.ShouldBe(original.Age);
        actual.IsRetired.ShouldBe(original.IsRetired);
        actual.Name.ShouldBeEquivalentTo(name);
    }

    [Test]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        SimpleWithBoolean? subject = default;

        // Act
        Func<SimpleWithBoolean> act = () => subject!.WithName("Avery Brooks");

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(nameof(subject));
    }
}