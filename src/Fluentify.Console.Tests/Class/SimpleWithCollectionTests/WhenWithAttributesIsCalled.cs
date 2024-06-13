namespace Fluentify.Console.Class.SimpleWithCollectionTests;

public sealed class WhenWithAttributesIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        SimpleWithCollection? subject = default;

        // Act
        Func<SimpleWithCollection> act = () => subject!.WithAttributes(new object());

        // Assert
        _ = act.Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(subject));
    }

    [Fact]
    public void GivenAttributesThenTheValueIsApplied()
    {
        // Arrange
        object first = new();
        object second = new();
        object[] expected = [first, second];

        var original = new SimpleWithCollection
        {
            Age = Random.Shared.Next(),
            Attributes = [first],
            Name = "Avery Brooks",
        };

        // Act
        SimpleWithCollection actual = original.WithAttributes(second);

        // Assert
        _ = actual.Should().NotBeSameAs(original);
        _ = actual.Age.Should().Be(original.Age);
        _ = actual.Attributes.Should().BeEquivalentTo(expected);
        _ = actual.Name.Should().Be(original.Name);
    }
}