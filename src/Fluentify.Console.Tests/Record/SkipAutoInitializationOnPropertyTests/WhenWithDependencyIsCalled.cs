namespace Fluentify.Console.Record.SkipAutoInitializationOnPropertyTests;

public sealed class WhenWithDependencyIsCalled
{
    [Test]
    public void GivenDependencyWhenBuilderIsUsedThenTheValueIsApplied()
    {
        // Arrange
        const string expected = "Avery Brooks";

        var subject = new SkipAutoInitializationOnProperty(
            Random.Shared.Next(),
            new SkipAutoInitializationOnProperty.Dependent
            {
                Name = "Patrick Stewart",
            });

        // Act
        SkipAutoInitializationOnProperty actual = subject.WithDependency(instance =>
        {
            instance.Name = expected;
            return instance;
        });

        // Assert
        actual.ShouldNotBeSameAs(subject);
        actual.Age.ShouldBe(subject.Age);
        actual.Dependency.Name.ShouldBe(expected);
    }

    [Test]
    public void GivenNullDependencyWhenBuilderIsUsedThenNotSupportedExceptionIsThrown()
    {
        // Arrange
        var subject = new SkipAutoInitializationOnProperty(Random.Shared.Next(), null!);

        // Act
        Action act = () => subject.WithDependency(instance =>
        {
            instance.Name = "Avery Brooks";
            return instance;
        });

        // Assert
        act.ShouldThrow<NotSupportedException>();
    }
}