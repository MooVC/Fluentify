namespace Fluentify.Console.Class.SimpleWithEnumerablesTests;

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
        _ = act.Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(subject));
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
        _ = actual.Should().NotBeSameAs(original);
        _ = actual.Age.Should().Be(original.Age);
        _ = actual.Attributes.Should().BeEquivalentTo(original.Attributes);
        _ = actual.Name.Should().Be(original.Name);
        _ = actual.Names.Should().BeEquivalentTo(expected);
        _ = actual.Numbers.Should().BeEquivalentTo(original.Numbers);
    }
}