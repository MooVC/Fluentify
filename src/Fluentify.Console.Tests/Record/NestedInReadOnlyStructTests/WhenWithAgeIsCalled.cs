namespace Fluentify.Console.Record.NestedInReadOnlyStructTests;

public sealed class WhenWithAgeIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        NestedInReadOnlyStruct.Simple? subject = default;

        // Act
        Func<NestedInReadOnlyStruct.Simple> act = () => subject!.WithAge(1);

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
        var original = new NestedInReadOnlyStruct.Simple
        {
            Age = Random.Shared.Next(),
            Attributes = [new(), new()],
            Name = "Avery Brooks",
        };

        // Act
        NestedInReadOnlyStruct.Simple actual = original.WithAge(age);

        // Assert
        actual.ShouldNotBeSameAs(original);
        actual.Age.ShouldBe(age);
        actual.Attributes.ShouldBeEquivalentTo(original.Attributes);
        actual.Name.ShouldBeEquivalentTo(original.Name);
    }
}