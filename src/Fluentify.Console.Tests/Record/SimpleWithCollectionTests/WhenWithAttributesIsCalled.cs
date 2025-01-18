namespace Fluentify.Console.Record.SimpleWithCollectionTests;

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

        var original = new SimpleWithCollection
        {
            Age = Random.Shared.Next(),
            Attributes = [first],
            Name = "Avery Brooks",
        };

        // Act
        SimpleWithCollection actual = original.WithAttributes(second);

        // Assert
        actual.ShouldNotBeSameAs(original);
        actual.Age.ShouldBe(original.Age);
        actual.Attributes.ShouldBe(expected);
        actual.Name.ShouldBe(original.Name);
    }
}