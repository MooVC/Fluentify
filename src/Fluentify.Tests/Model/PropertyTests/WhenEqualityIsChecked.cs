namespace Fluentify.Model.PropertyTests;

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
            IsIgnored = true,
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
            IsIgnored = true,
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
        areEqual.ShouldBeTrue();
    }

    [Fact]
    public void GivenDifferentAccessibilityThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            IsIgnored = true,
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
            IsIgnored = true,
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
        areEqual.ShouldBeFalse();
    }

    [Fact]
    public void GivenDifferentDescriptorThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor1",
            IsIgnored = true,
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
            IsIgnored = true,
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
        areEqual.ShouldBeFalse();
    }

    [Fact]
    public void GivenDifferentIsIgnoredThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            IsIgnored = true,
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
            IsIgnored = false,
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
        areEqual.ShouldBeFalse();
    }

    [Fact]
    public void GivenDifferentIsHiddenThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            IsHidden = true,
            IsIgnored = true,
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
            IsHidden = false,
            IsIgnored = true,
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
        areEqual.ShouldBeFalse();
    }

    [Fact]
    public void GivenDifferentKindThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            IsIgnored = true,
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
            IsIgnored = true,
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
        areEqual.ShouldBeFalse();
    }

    [Fact]
    public void GivenDifferentNameThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            IsIgnored = true,
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
            IsIgnored = true,
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
        areEqual.ShouldBeFalse();
    }

    [Fact]
    public void GivenOneInstanceIsNullThenTheyAreNotEqual()
    {
        // Arrange
        var instance = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            IsIgnored = true,
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
        areEqual.ShouldBeFalse();
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
        areEqual.ShouldBeTrue();
    }

    [Fact]
    public void GivenSameReferenceThenTheyAreEqual()
    {
        // Arrange
        var instance = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "descriptor",
            IsIgnored = true,
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
        areEqual.ShouldBeTrue();
    }

    private protected abstract bool AreEqual(Property? instance1, Property? instance2);
}