namespace Fluentify.Console.Record.SkipAutoInitializationOnTypeTests;

public sealed class WhenWithDependencyIsCalled
{
    [Fact]
    public void GivenNullDependencyWhenBuilderIsUsedThenNotSupportedExceptionIsThrown()
    {
        // Arrange
        var subject = new SkipAutoInitializationOnType(Random.Shared.Next(), null!);

        // Act
        Action act = () => subject.WithDependency(instance =>
        {
            instance.Name = "Avery Brooks";
            return instance;
        });

        // Assert
        act.ShouldThrow<NotSupportedException>();
    }

    [Fact]
    public void GivenDependencyWhenBuilderIsUsedThenTheValueIsApplied()
    {
        // Arrange
        const string expected = "Avery Brooks";

        var subject = new SkipAutoInitializationOnType(
            Random.Shared.Next(),
            new SkipAutoInitializationOnType.Dependent
            {
                Name = "Patrick Stewart",
            });

        // Act
        SkipAutoInitializationOnType actual = subject.WithDependency(instance =>
        {
            instance.Name = expected;
            return instance;
        });

        // Assert
        actual.ShouldNotBeSameAs(subject);
        actual.Age.ShouldBe(subject.Age);
        actual.Dependency.Name.ShouldBe(expected);
    }
}