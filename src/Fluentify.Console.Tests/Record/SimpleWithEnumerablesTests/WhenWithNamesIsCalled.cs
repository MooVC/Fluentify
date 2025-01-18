namespace Fluentify.Console.Record.SimpleWithEnumerablesTests;

public sealed class WhenWithNamesIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        SimpleWithEnumerables? subject = default;

        // Act
        Func<SimpleWithEnumerables> act = () => subject!.WithNames("Avery Brooks");

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(nameof(subject));
    }

    [Fact]
    public void GivenAttributesThenTheValueIsApplied()
    {
        // Arrange
        string first = "Patrick Stewart";
        string second = "Kate Mulgrew";
        string[] expected = [first, second];

        var original = new SimpleWithEnumerables
        {
            Age = Random.Shared.Next(),
            Attributes = [new(), new()],
            Name = "Avery Brooks",
            Names = [first],
            Numbers = [1, 2],
        };

        // Act
        SimpleWithEnumerables actual = original.WithNames(second);

        // Assert
        actual.ShouldNotBeSameAs(original);
        actual.Age.ShouldBe(original.Age);
        actual.Attributes.ShouldBeEquivalentTo(original.Attributes);
        actual.Name.ShouldBe(original.Name);
        actual.Names.ShouldBeEquivalentTo(expected);
        actual.Numbers.ShouldBeEquivalentTo(original.Numbers);
    }
}