namespace Fluentify.Console.Class.Ignore.TwoOfThreeTests;

public sealed class WhenWithAttributesIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        TwoOfThree? subject = default;

        // Act
        Func<TwoOfThree> act = () => subject!.WithAttributes([new()]);

        // Assert
        _ = act.Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(subject));
    }

    [Fact]
    public void GivenAttributesThenTheValueIsApplied()
    {
        // Arrange
        object[] attributes = [new()];

        var original = new TwoOfThree
        {
            Age = Random.Shared.Next(),
            Attributes = [],
            Name = "Avery Brooks",
        };

        // Act
        TwoOfThree actual = original.WithAttributes(attributes);

        // Assert
        _ = actual.Should().NotBeSameAs(original);
        _ = actual.Age.Should().Be(original.Age);
        _ = actual.Attributes.Should().BeEquivalentTo(attributes);
        _ = actual.Name.Should().Be(original.Name);
    }
}