namespace Fluentify.Console.Record.NestedInRefStructTests;

public sealed class WhenWithAttributesIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        NestedInRefStruct.Simple? subject = default;

        // Act
        Func<NestedInRefStruct.Simple> act = () => subject!.WithAttributes(new object());

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(nameof(subject));
    }

    [Fact]
    public void GivenAttributesThenTheValueIsApplied()
    {
        // Arrange
        object[] attributes = [new()];

        var original = new NestedInRefStruct.Simple
        {
            Age = Random.Shared.Next(),
            Attributes = [],
            Name = "Avery Brooks",
        };

        // Act
        NestedInRefStruct.Simple actual = original.WithAttributes(attributes);

        // Assert
        actual.ShouldNotBeSameAs(original);
        actual.Age.ShouldBe(original.Age);
        actual.Attributes.ShouldBeEquivalentTo(attributes);
        actual.Name.ShouldBe(original.Name);
    }
}