namespace Fluentify.Console.Class.SkipAutoInitializationOnTypeTests;

public sealed class WhenWithDependencyIsCalled
{
    [Fact]
    public void GivenNullDependencyWhenBuilderIsUsedThenNotSupportedExceptionIsThrown()
    {
        // Arrange
        var subject = new SkipAutoInitializationOnType
        {
            Age = Random.Shared.Next(),
            Dependency = null!,
        };

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

        var subject = new SkipAutoInitializationOnType
        {
            Age = Random.Shared.Next(),
            Dependency = new SkipAutoInitializationOnType.Dependent
            {
                Name = "Patrick Stewart",
            },
        };

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