namespace Fluentify.Console.Record.SimpleWithBooleanTests;

public sealed class WhenIsRetiredIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        SimpleWithBoolean? subject = default;

        // Act
        Func<SimpleWithBoolean> act = () => subject!.IsRetired(true);

        // Assert
        _ = act.Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(subject));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenAValueThenTheValueIsApplied(bool isRetired)
    {
        // Arrange
        var original = new SimpleWithBoolean
        {
            Age = Random.Shared.Next(),
            IsRetired = !isRetired,
            Name = "Avery Brooks",
        };

        // Act
        SimpleWithBoolean actual = original.IsRetired(isRetired);

        // Assert
        _ = actual.Should().NotBeSameAs(original);
        _ = actual.Age.Should().Be(original.Age);
        _ = actual.IsRetired.Should().Be(isRetired);
        _ = actual.Name.Should().Be(original.Name);
    }

    [Fact]
    public void GivenANullValueThenTheValueIsApplied()
    {
        // Arrange
        var original = new SimpleWithBoolean
        {
            Age = Random.Shared.Next(),
            IsRetired = true,
            Name = "Avery Brooks",
        };

        // Act
        SimpleWithBoolean actual = original.IsRetired(default);

        // Assert
        _ = actual.Should().NotBeSameAs(original);
        _ = actual.Age.Should().Be(original.Age);
        _ = actual.IsRetired.Should().BeNull();
        _ = actual.Name.Should().Be(original.Name);
    }
}