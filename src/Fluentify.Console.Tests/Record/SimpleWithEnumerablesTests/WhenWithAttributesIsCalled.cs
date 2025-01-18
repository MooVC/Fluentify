namespace Fluentify.Console.Record.SimpleWithEnumerablesTests;

public sealed class WhenWithAttributesIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        SimpleWithEnumerables? subject = default;

        // Act
        Func<SimpleWithEnumerables> act = () => subject!.WithAttributes(new object());

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(nameof(subject));
    }

    [Fact]
    public void GivenAttributesThenTheValueIsApplied()
    {
        // Arrange
        object first = new();
        object second = new();
        object[] expected = [first, second];

        var original = new SimpleWithEnumerables
        {
            Age = Random.Shared.Next(),
            Attributes = [first],
            Name = "Avery Brooks",
            Names = ["Patrick Stewart", "Kate Mulgrew"],
            Numbers = [1, 2],
        };

        // Act
        SimpleWithEnumerables actual = original.WithAttributes(second);

        // Assert
        actual.ShouldNotBeSameAs(original);
        actual.Age.ShouldBe(original.Age);
        actual.Attributes.ShouldBeEquivalentTo(expected);
        actual.Name.ShouldBe(original.Name);
        actual.Names.ShouldBeEquivalentTo(original.Names);
        actual.Numbers.ShouldBeEquivalentTo(original.Numbers);
    }
}