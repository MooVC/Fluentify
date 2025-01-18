namespace Fluentify.Console.Class.SimpleWithEnumerablesTests;

public sealed class WhenWithNumbersIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        SimpleWithEnumerables? subject = default;

        // Act
        Func<SimpleWithEnumerables> act = () => subject!.WithNumbers(1, 2);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(nameof(subject));
    }

    [Fact]
    public void GivenAttributesThenTheValueIsApplied()
    {
        // Arrange
        int first = 1;
        int second = 2;
        int[] expected = [first, second];

        var original = new SimpleWithEnumerables
        {
            Age = Random.Shared.Next(),
            Attributes = [new(), new()],
            Name = "Avery Brooks",
            Names = ["Patrick Stewart", "Kate Mulgrew"],
            Numbers = [first],
        };

        // Act
        SimpleWithEnumerables actual = original.WithNumbers(second);

        // Assert
        actual.ShouldNotBeSameAs(original);
        actual.Age.ShouldBe(original.Age);
        actual.Attributes.ShouldBeEquivalentTo(original.Attributes);
        actual.Name.ShouldBe(original.Name);
        actual.Names.ShouldBeEquivalentTo(original.Names);
        actual.Numbers.ShouldBeEquivalentTo(expected);
    }
}