namespace Fluentify.Console.Record.SingleTests;

public sealed class WhenWithAgeIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        Single? subject = default;

        // Act
        Func<Single> act = () => subject!.WithAge(1);

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
        var original = new Single
        {
            Age = Random.Shared.Next(),
        };

        // Act
        Single actual = original.WithAge(age);

        // Assert
        _ = actual.Should().NotBeSameAs(original);
        _ = actual.Age.Should().Be(age);
    }
}