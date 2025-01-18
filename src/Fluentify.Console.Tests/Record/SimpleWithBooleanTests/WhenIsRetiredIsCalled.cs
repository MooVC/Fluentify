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
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(nameof(subject));
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
        actual.ShouldNotBeSameAs(original);
        actual.Age.ShouldBe(original.Age);
        actual.IsRetired.ShouldBe(isRetired);
        actual.Name.ShouldBe(original.Name);
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
        actual.ShouldNotBeSameAs(original);
        actual.Age.ShouldBe(original.Age);
        actual.IsRetired.ShouldBeNull();
        actual.Name.ShouldBe(original.Name);
    }
}