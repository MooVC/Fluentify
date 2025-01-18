namespace Fluentify.Console.Class.SelfDescriptor.OnOptionalTests;

public sealed class WhenAttributesIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        OnOptional? subject = default;

        // Act
        Func<OnOptional> act = () => subject!.Attributes(new object());

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(nameof(subject));
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
        OnOptional actual = original.Attributes(attributes);

        // Assert
        actual.ShouldNotBeSameAs(original);
        actual.Age.ShouldBe(original.Age);
        actual.Attributes.ShouldBeEquivalentTo(attributes);
        actual.Name.ShouldBe(original.Name);
    }
}