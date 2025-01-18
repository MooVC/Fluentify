namespace Fluentify.Console.Class.SimpleWithBooleanTests;

public sealed class WhenWithAgeIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        SimpleWithBoolean? subject = default;

        // Act
        Func<SimpleWithBoolean> act = () => subject!.WithAge(1);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .ParamName.ShouldBe(nameof(subject));
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
        var original = new SimpleWithBoolean
        {
            Age = Random.Shared.Next(),
            IsRetired = true,
            Name = "Avery Brooks",
        };

        // Act
        SimpleWithBoolean actual = original.WithAge(age);

        // Assert
        actual.ShouldNotBeSameAs(original);
        actual.Age.ShouldBe(age);
        actual.IsRetired.ShouldBe(original.IsRetired);
        actual.Name.ShouldBeEquivalentTo(original.Name);
    }
}