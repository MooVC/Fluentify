namespace Fluentify.Console.Class.SkipAutoInstantiationTests;

using Fluentify.Console.Class.SkipAutoInstantiation;

public sealed class WhenWithDependencyIsCalled
{
    [Fact]
    public void GivenDependencyThenTheValueIsApplied()
    {
        // Arrange
        var dependency = new Example.DependencySettings
        {
            ConnectionString = "Server=Primary;",
        };

        var original = new Example
        {
            Dependency = new Example.DependencySettings
            {
                ConnectionString = "Server=Original;",
            },
            Name = "Original",
        };

        // Act
        Example actual = original.WithDependency(dependency);

        // Assert
        actual.ShouldNotBeSameAs(original);
        actual.Dependency.ShouldBe(dependency);
        actual.Name.ShouldBe(original.Name);
    }
}