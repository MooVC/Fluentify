namespace Fluentify.Model.PropertyTests;

using Microsoft.CodeAnalysis;

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
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        areNotEqual.ShouldBeFalse();
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
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        areNotEqual.ShouldBeTrue();
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
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        areNotEqual.ShouldBeTrue();
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
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        areNotEqual.ShouldBeTrue();
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
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        areNotEqual.ShouldBeTrue();
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
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        areNotEqual.ShouldBeTrue();
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
        bool areNotEqual = AreNotEqual(instance, default);

        // Assert
        areNotEqual.ShouldBeTrue();
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
        areNotEqual.ShouldBeFalse();
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
        bool areNotEqual = AreNotEqual(instance, instance);

        // Assert
        areNotEqual.ShouldBeFalse();
    }

    private protected abstract bool AreNotEqual(Property? instance1, Property? instance2);
}