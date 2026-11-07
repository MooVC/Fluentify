namespace Fluentify.Console.Record.SingleGenericTests;

public sealed class WhenWithNameIsCalled
{
    [Test]
    [Arguments("Avery Brooks")]
    [Arguments("")]
    [Arguments(" ")]
    public void GivenANameThenTheValueIsApplied(string name)
    {
        // Arrange
        var original = new SingleGeneric<List<int>>
        {
            Age = Random.Shared.Next(),
            Attributes = [1, 2],
            Name = "Patrick Stewart",
        };

        // Act
        SingleGeneric<List<int>> actual = original.WithName(name);

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
        SingleGeneric<List<int>>? subject = default;

        // Act
        Func<SingleGeneric<List<int>>> act = () => subject!.WithName("Avery Brooks");

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(nameof(subject));
    }
}