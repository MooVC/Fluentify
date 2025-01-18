namespace Fluentify.Console.Record.SimpleWithEnumerablesTests;

public sealed class WhenWithNameIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        SimpleWithEnumerables? subject = default;

        // Act
        Func<SimpleWithEnumerables> act = () => subject!.WithName("Avery Brooks");

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
        var original = new SimpleWithEnumerables
        {
            Age = Random.Shared.Next(),
            Attributes = [new(), new()],
            Name = "Patrick Stewart",
            Names = ["Patrick Stewart", "Kate Mulgrew"],
            Numbers = [1, 2],
        };

        // Act
        SimpleWithEnumerables actual = original.WithName(name);

        // Assert
        actual.ShouldNotBeSameAs(original);
        actual.Age.ShouldBe(original.Age);
        actual.Attributes.ShouldBeEquivalentTo(original.Attributes);
        actual.Name.ShouldBeEquivalentTo(name);
        actual.Names.ShouldBeEquivalentTo(original.Names);
        actual.Numbers.ShouldBeEquivalentTo(original.Numbers);
    }
}