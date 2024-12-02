namespace Fluentify.Console.Record.NestedInInterfaceTests;

public sealed class WhenWithAttributesIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        NestedInInterface.Simple? subject = default;

        // Act
        Func<NestedInInterface.Simple> act = () => subject!.WithAttributes(new object());

        // Assert
        _ = act.Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(subject));
    }

    [Fact]
    public void GivenAttributesThenTheValueIsApplied()
    {
        // Arrange
        object[] attributes = [new()];

        var original = new NestedInInterface.Simple
        {
            Age = Random.Shared.Next(),
            Attributes = [],
            Name = "Avery Brooks",
        };

        // Act
        NestedInInterface.Simple actual = original.WithAttributes(attributes);

        // Assert
        _ = actual.Should().NotBeSameAs(original);
        _ = actual.Age.Should().Be(original.Age);
        _ = actual.Attributes.Should().BeEquivalentTo(attributes);
        _ = actual.Name.Should().Be(original.Name);
    }
}