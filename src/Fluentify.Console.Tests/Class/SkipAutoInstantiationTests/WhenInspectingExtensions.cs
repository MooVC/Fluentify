namespace Fluentify.Console.Class.SkipAutoInstantiationTests;

using System;
using System.Reflection;

public sealed class WhenInspectingExtensions
{
    [Fact]
    public void GivenSkipAutoInstantiationAttributeThenBuilderExtensionIsNotGenerated()
    {
        // Arrange
        Type extensions = typeof(Class.SkipAutoInstantiation.Example)
            .Assembly
            .GetType("Fluentify.Console.Class.SkipAutoInstantiation.ExampleExtensions")!
            .ShouldNotBeNull();

        // Act
        MethodInfo? builder = extensions
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .SingleOrDefault(method => method.Name == "WithDependency"
                && method.GetParameters().Any(parameter => parameter.ParameterType.IsGenericType
                    && parameter.ParameterType.GetGenericTypeDefinition() == typeof(Func<,>)));

        // Assert
        builder.ShouldBeNull();
    }
}