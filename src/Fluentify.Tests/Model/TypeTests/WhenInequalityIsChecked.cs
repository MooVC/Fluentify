namespace Fluentify.Model.TypeTests;

using FluentAssertions;
using Microsoft.CodeAnalysis;
using Xunit;

public abstract class WhenInequalityIsChecked
{
    [Fact]
    public void GivenIdenticalInstancesThenTheyAreEqual()
    {
        // Arrange
        var instance1 = new Type
        {
            Name = "TypeName",
        };
        var instance2 = new Type
        {
            Name = "TypeName",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        _ = areNotEqual.Should().BeFalse();
    }

    [Fact]
    public void GivenDifferentIsNullableThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Type
        {
            IsNullable = true,
            Name = "TypeName",
        };
        var instance2 = new Type
        {
            Name = "TypeName",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        _ = areNotEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentNameThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Type
        {
            Name = "TypeName1",
        };
        var instance2 = new Type
        {
            Name = "TypeName2",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        _ = areNotEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenOneInstanceIsNullThenTheyAreNotEqual()
    {
        // Arrange
        var instance = new Type
        {
            Name = "TypeName",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance, default);

        // Assert
        _ = areNotEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenBothInstancesAreNullThenTheyAreEqual()
    {
        // Arrange
        Type? instance1 = default;
        Type? instance2 = default;

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        _ = areNotEqual.Should().BeFalse();
    }

    [Fact]
    public void GivenSameReferenceThenTheyAreEqual()
    {
        // Arrange
        var instance = new Type
        {
            Name = "TypeName",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance, instance);

        // Assert
        _ = areNotEqual.Should().BeFalse();
    }

    private protected abstract bool AreNotEqual(Type? instance1, Type? instance2);
}