namespace Fluentify.Console.Record.NestedInClassTests;

public sealed class WhenWithAttributesIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        NestedInClass.Simple? subject = default;

        // Act
        Func<NestedInClass.Simple> act = () => subject!.WithAttributes(new object());

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(nameof(subject));
    }

    [Fact]
    public void GivenAttributesThenTheValueIsApplied()
    {
        // Arrange
        object[] attributes = [new()];

        var original = new NestedInClass.Simple
        {
            Age = Random.Shared.Next(),
            Attributes = [],
            Name = "Avery Brooks",
        };

        // Act
        NestedInClass.Simple actual = original.WithAttributes(attributes);

        // Assert
        actual.ShouldNotBeSameAs(original);
        actual.Age.ShouldBe(original.Age);
        actual.Attributes.ShouldBeEquivalentTo(attributes);
        actual.Name.ShouldBe(original.Name);
    }
}