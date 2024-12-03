namespace Fluentify.Console.Record.NestedInRefStructTests;

public sealed class WhenWithNameIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        NestedInRefStruct.Simple? subject = default;

        // Act
        Func<NestedInRefStruct.Simple> act = () => subject!.WithName("Avery Brooks");

        // Assert
        _ = act.Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(subject));
    }

    [Theory]
    [InlineData("Avery Brooks")]
    [InlineData("")]
    [InlineData(" ")]
    public void GivenANameThenTheValueIsApplied(string name)
    {
        // Arrange
        var original = new NestedInRefStruct.Simple
        {
            Age = Random.Shared.Next(),
            Attributes = [new(), new()],
            Name = "Patrick Stewart",
        };

        // Act
        NestedInRefStruct.Simple actual = original.WithName(name);

        // Assert
        _ = actual.Should().NotBeSameAs(original);
        _ = actual.Age.Should().Be(original.Age);
        _ = actual.Attributes.Should().BeEquivalentTo(original.Attributes);
        _ = actual.Name.Should().BeEquivalentTo(name);
    }
}