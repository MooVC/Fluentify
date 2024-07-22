namespace Fluentify.Console.Record.SimpleWithEnumerablesTests;

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
        _ = act.Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(subject));
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
        _ = actual.Should().NotBeSameAs(original);
        _ = actual.Age.Should().Be(original.Age);
        _ = actual.Attributes.Should().BeEquivalentTo(original.Attributes);
        _ = actual.Name.Should().Be(original.Name);
        _ = actual.Names.Should().BeEquivalentTo(original.Names);
        _ = actual.Numbers.Should().BeEquivalentTo(expected);
    }
}