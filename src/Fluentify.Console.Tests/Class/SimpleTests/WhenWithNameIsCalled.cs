namespace Fluentify.Console.Class.SimpleTests;

public sealed class WhenWithNameIsCalled
{
    [Test]
    [Arguments("Avery Brooks")]
    [Arguments("")]
    [Arguments(" ")]
    public void GivenANameThenTheValueIsApplied(string name)
    {
        // Arrange
        var original = new Simple
        {
            Age = Random.Shared.Next(),
            Attributes = [new(), new()],
            Name = "Patrick Stewart",
        };

        // Act
        Simple actual = original.WithName(name);

        // Assert
        actual.ShouldNotBeSameAs(original);
        actual.Age.ShouldBe(original.Age);
        actual.Attributes.ShouldBeEquivalentTo(original.Attributes);
        actual.Name.ShouldBeEquivalentTo(name);
    }

    [Test]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        Simple? subject = default;

        // Act
        Func<Simple> act = () => subject!.WithName("Avery Brooks");

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(nameof(subject));
    }
}