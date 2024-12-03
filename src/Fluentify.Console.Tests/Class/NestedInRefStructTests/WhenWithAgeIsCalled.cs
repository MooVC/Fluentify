namespace Fluentify.Console.Class.NestedInRefStructTests;

public sealed class WhenWithAgeIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        NestedInRefStruct.Simple? subject = default;

        // Act
        Func<NestedInRefStruct.Simple> act = () => subject!.WithAge(1);

        // Assert
        _ = act.Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(subject));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void GivenAnAgeThenTheValueIsApplied(int age)
    {
        // Arrange
        var original = new NestedInRefStruct.Simple
        {
            Age = Random.Shared.Next(),
            Attributes = [new(), new()],
            Name = "Avery Brooks",
        };

        // Act
        NestedInRefStruct.Simple actual = original.WithAge(age);

        // Assert
        _ = actual.Should().NotBeSameAs(original);
        _ = actual.Age.Should().Be(age);
        _ = actual.Attributes.Should().BeEquivalentTo(original.Attributes);
        _ = actual.Name.Should().BeEquivalentTo(original.Name);
    }
}