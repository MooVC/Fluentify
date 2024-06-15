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
        _ = act.Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(subject));
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
        _ = actual.Should().NotBeSameAs(original);
        _ = actual.Age.Should().Be(original.Age);
        _ = actual.Attributes.Should().BeEquivalentTo(original.Attributes);
        _ = actual.Name.Should().BeEquivalentTo(name);
        _ = actual.Names.Should().BeEquivalentTo(original.Names);
        _ = actual.Numbers.Should().BeEquivalentTo(original.Numbers);
    }
}