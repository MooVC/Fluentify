namespace Fluentify.Model.PropertyTests;

using FluentAssertions;
using Microsoft.CodeAnalysis;
using Xunit;

public abstract class WhenEqualityIsChecked
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
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        _ = areEqual.Should().BeTrue();
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
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        _ = areEqual.Should().BeFalse();
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
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        _ = areEqual.Should().BeFalse();
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
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        _ = areEqual.Should().BeFalse();
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
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        _ = areEqual.Should().BeFalse();
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
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        _ = areEqual.Should().BeFalse();
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
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        _ = areEqual.Should().BeFalse();
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
        bool areEqual = AreEqual(instance, default);

        // Assert
        _ = areEqual.Should().BeFalse();
    }

    [Fact]
    public void GivenBothInstancesAreNullThenTheyAreEqual()
    {
        // Arrange
        Property? instance1 = default;
        Property? instance2 = default;

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        _ = areEqual.Should().BeTrue();
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
        bool areEqual = AreEqual(instance, instance);

        // Assert
        _ = areEqual.Should().BeTrue();
    }

    private protected abstract bool AreEqual(Property? instance1, Property? instance2);
}