namespace Fluentify.Console.Record.MultipleGenericsTests;

public sealed class WhenWithAttributesIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        MultipleGenerics<int, object, List<string>>? subject = default;

        // Act
        Func<MultipleGenerics<int, object, List<string>>> act = () => subject!.WithAttributes(["Null"]);

        // Assert
        _ = act.Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(subject));
    }

    [Fact]
    public void GivenAttributesThenTheValueIsApplied()
    {
        // Arrange
        List<string> attributes = ["Captain", "USS Defiant"];

        var original = new MultipleGenerics<int, object, List<string>>
        {
            Age = Random.Shared.Next(),
            Attributes = [],
            Name = "Avery Brooks",
        };

        // Act
        MultipleGenerics<int, object, List<string>> actual = original.WithAttributes(attributes);

        // Assert
        _ = actual.Should().NotBeSameAs(original);
        _ = actual.Age.Should().Be(original.Age);
        _ = actual.Attributes.Should().BeEquivalentTo(attributes);
        _ = actual.Name.Should().Be(original.Name);
    }
}