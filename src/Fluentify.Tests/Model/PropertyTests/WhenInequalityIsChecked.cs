namespace Fluentify.Model.PropertyTests;

using FluentAssertions;
using Microsoft.CodeAnalysis;
using Xunit;

public abstract class WhenInequalityIsChecked
{
    [Fact]
    public void GivenIdenticalInstancesThenTheyAreEqual()
    {
        // Arrange
        var instance1 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            IsBuildable = true,
            IsNullable = false,
            Name = "PropertyName",
            Type = "string",
        };
        var instance2 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            IsBuildable = true,
            IsNullable = false,
            Name = "PropertyName",
            Type = "string",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        _ = areNotEqual.Should().BeFalse();
    }

    [Fact]
    public void GivenDifferentAccessibilityThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            IsBuildable = true,
            IsNullable = false,
            Name = "PropertyName",
            Type = "string",
        };
        var instance2 = new Property
        {
            Accessibility = Accessibility.Private,
            Descriptor = "descriptor",
            IsBuildable = true,
            IsNullable = false,
            Name = "PropertyName",
            Type = "string",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        _ = areNotEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentDescriptorThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor1",
            IsBuildable = true,
            IsNullable = false,
            Name = "PropertyName",
            Type = "string",
        };
        var instance2 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor2",
            IsBuildable = true,
            IsNullable = false,
            Name = "PropertyName",
            Type = "string",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        _ = areNotEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentIsBuildableThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            IsBuildable = true,
            IsNullable = false,
            Name = "PropertyName",
            Type = "string",
        };
        var instance2 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            IsBuildable = false,
            IsNullable = false,
            Name = "PropertyName",
            Type = "string",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        _ = areNotEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentIsNullableThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            IsBuildable = true,
            IsNullable = false,
            Name = "PropertyName",
            Type = "string",
        };
        var instance2 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            IsBuildable = true,
            IsNullable = true,
            Name = "PropertyName",
            Type = "string",
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
        var instance1 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            IsBuildable = true,
            IsNullable = false,
            Name = "PropertyName1",
            Type = "string",
        };
        var instance2 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            IsBuildable = true,
            IsNullable = false,
            Name = "PropertyName2",
            Type = "string",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        _ = areNotEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentTypeThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            IsBuildable = true,
            IsNullable = false,
            Name = "PropertyName",
            Type = "string",
        };
        var instance2 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            IsBuildable = true,
            IsNullable = false,
            Name = "PropertyName",
            Type = "int",
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
        var instance = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            IsBuildable = true,
            IsNullable = false,
            Name = "PropertyName",
            Type = "string",
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
        Property? instance1 = default;
        Property? instance2 = default;

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        _ = areNotEqual.Should().BeFalse();
    }

    [Fact]
    public void GivenSameReferenceThenTheyAreEqual()
    {
        // Arrange
        var instance = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            IsBuildable = true,
            IsNullable = false,
            Name = "PropertyName",
            Type = "string",
        };

        // Act
        bool areNotEqual = AreNotEqual(instance, instance);

        // Assert
        _ = areNotEqual.Should().BeFalse();
    }

    private protected abstract bool AreNotEqual(Property? instance1, Property? instance2);
}