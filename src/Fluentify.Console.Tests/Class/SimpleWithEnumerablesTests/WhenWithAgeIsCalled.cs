namespace Fluentify.Console.Class.SimpleWithEnumerablesTests;

public sealed class WhenWithAgeIsCalled
{
    [Fact]
    public void GivenNullSubjectThenArgumentNullExceptionIsThrown()
    {
        // Arrange
        SimpleWithEnumerables? subject = default;

        // Act
        Func<SimpleWithEnumerables> act = () => subject!.WithAge(1);

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
        var original = new SimpleWithEnumerables
        {
            Age = Random.Shared.Next(),
            Attributes = [new(), new()],
            Name = "Avery Brooks",
            Names = ["Patrick Stewart", "Kate Mulgrew"],
            Numbers = [1, 2],
        };

        // Act
        SimpleWithEnumerables actual = original.WithAge(age);

        // Assert
        actual.ShouldNotBeSameAs(original);
        actual.Age.ShouldBe(age);
        actual.Attributes.ShouldBeEquivalentTo(original.Attributes);
        actual.Name.ShouldBeEquivalentTo(original.Name);
        actual.Names.ShouldBeEquivalentTo(original.Names);
        actual.Numbers.ShouldBeEquivalentTo(original.Numbers);
    }
}