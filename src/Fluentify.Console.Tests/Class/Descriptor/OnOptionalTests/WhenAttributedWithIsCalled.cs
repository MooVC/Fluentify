namespace Fluentify.Console.Class.Descriptor.OnOptionalTests;

public sealed class WhenAttributedWithIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        OnOptional? subject = default;

        // Act
        Func<OnOptional> act = () => subject!.AttributedWith([new()]);

        // Assert
        _ = act.Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(subject));
    }

    [Fact]
    public void GivenAttributesThenTheValueIsApplied()
    {
        // Arrange
        object[] attributes = [new()];

        var original = new OnOptional
        {
            Age = Random.Shared.Next(),
            Attributes = [],
            Name = "Avery Brooks",
        };

        // Act
        OnOptional actual = original.AttributedWith(attributes);

        // Assert
        _ = actual.Should().NotBeSameAs(original);
        _ = actual.Age.Should().Be(original.Age);
        _ = actual.Attributes.Should().BeEquivalentTo(attributes);
        _ = actual.Name.Should().Be(original.Name);
    }
}