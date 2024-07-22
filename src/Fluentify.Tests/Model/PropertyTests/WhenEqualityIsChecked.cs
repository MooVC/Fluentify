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
            Kind = new()
            {
                Type = new()
                {
                    Name = "string",
                },
            },
            Name = "PropertyName",
        };
        var instance2 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            Kind = new()
            {
                Type = new()
                {
                    Name = "string",
                },
            },
            Name = "PropertyName",
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
            Kind = new()
            {
                Type = new()
                {
                    Name = "string",
                },
            },
            Name = "PropertyName",
        };
        var instance2 = new Property
        {
            Accessibility = Accessibility.Private,
            Descriptor = "descriptor",
            Kind = new()
            {
                Type = new()
                {
                    Name = "string",
                },
            },
            Name = "PropertyName",
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
            Kind = new()
            {
                Type = new()
                {
                    Name = "string",
                },
            },
            Name = "PropertyName",
        };
        var instance2 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor2",
            Kind = new()
            {
                Type = new()
                {
                    Name = "string",
                },
            },
            Name = "PropertyName",
        };

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        _ = areEqual.Should().BeFalse();
    }

    [Fact]
    public void GivenDifferentKindThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            Kind = new()
            {
                Type = new()
                {
                    Name = "string",
                },
            },
            Name = "PropertyName",
        };
        var instance2 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            Kind = new()
            {
                Type = new()
                {
                    Name = "int",
                },
            },
            Name = "PropertyName",
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
            Kind = new()
            {
                Type = new()
                {
                    Name = "string",
                },
            },
            Name = "PropertyName1",
        };
        var instance2 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            Kind = new()
            {
                Type = new()
                {
                    Name = "string",
                },
            },
            Name = "PropertyName2",
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
            Kind = new()
            {
                Type = new()
                {
                    Name = "string",
                },
            },
            Name = "PropertyName",
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
            Kind = new()
            {
                Type = new()
                {
                    Name = "string",
                },
            },
            Name = "PropertyName",
        };

        // Act
        bool areEqual = AreEqual(instance, instance);

        // Assert
        _ = areEqual.Should().BeTrue();
    }

    private protected abstract bool AreEqual(Property? instance1, Property? instance2);
}