namespace Fluentify.Console.Class.Descriptor.OnIgnoredTests;

public sealed class WhenWithAttributesIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        OnIgnored? subject = default;

        // Act
        Func<OnIgnored> act = () => subject!.WithAttributes([new()]);

        // Assert
        _ = act.Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(subject));
    }

    [Fact]
    public void GivenAttributesThenTheValueIsApplied()
    {
        // Arrange
        object[] attributes = [new()];

        var original = new OnIgnored
        {
            Age = Random.Shared.Next(),
            Attributes = [],
            Name = "Avery Brooks",
        };

        // Act
        OnIgnored actual = original.WithAttributes(attributes);

        // Assert
        _ = actual.Should().NotBeSameAs(original);
        _ = actual.Age.Should().Be(original.Age);
        _ = actual.Attributes.Should().BeEquivalentTo(attributes);
        _ = actual.Name.Should().Be(original.Name);
    }
}