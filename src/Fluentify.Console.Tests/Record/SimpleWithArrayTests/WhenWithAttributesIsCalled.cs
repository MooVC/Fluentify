namespace Fluentify.Console.Record.SimpleWithArrayTests;

public sealed class WhenWithAttributesIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        SimpleWithArray? subject = default;

        // Act
        Func<SimpleWithArray> act = () => subject!.WithAttributes(new object());

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

        var original = new SimpleWithArray
        {
            Age = Random.Shared.Next(),
            Attributes = [first],
            Name = "Avery Brooks",
        };

        // Act
        SimpleWithArray actual = original.WithAttributes(second);

        // Assert
        actual.ShouldNotBeSameAs(original);
        actual.Age.ShouldBe(original.Age);
        actual.Attributes.ShouldBeEquivalentTo(expected);
        actual.Name.ShouldBe(original.Name);
    }
}