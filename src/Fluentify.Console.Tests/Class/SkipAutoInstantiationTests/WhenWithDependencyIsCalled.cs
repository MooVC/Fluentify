namespace Fluentify.Console.Class.SkipAutoInstantiationTests;

using Fluentify.Console.Class.SkipAutoInstantiation;

public sealed class WhenWithDependencyIsCalled
{
    [Fact]
    public void GivenDependencyThenTheValueIsApplied()
    {
        // Arrange
        var dependency = new Class.SkipAutoInstantiation.Example.DependencySettings
        {
            ConnectionString = "Server=Primary;",
        };

        var original = new Class.SkipAutoInstantiation.Example
        {
            Dependency = new Class.SkipAutoInstantiation.Example.DependencySettings
            {
                ConnectionString = "Server=Original;",
            },
            Name = "Original",
        };

        // Act
        Class.SkipAutoInstantiation.Example actual = original.WithDependency(dependency);

        // Assert
        actual.ShouldNotBeSameAs(original);
        actual.Dependency.ShouldBe(dependency);
        actual.Name.ShouldBe(original.Name);
    }
}