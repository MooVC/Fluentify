namespace Fluentify.Model.SubjectTests;

using System.Collections.Generic;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Xunit;

public abstract class WhenEqualityIsChecked
{
    [Fact]
    public void GivenIdenticalInstancesThenTheyAreEqual()
    {
        // Arrange
        var generics = new List<Generic>
        {
            new()
            {
                Constraints = ["constraint1"],
                Name = "GenericName1",
            },
            new()
            {
                Constraints = ["constraint2"],
                Name = "GenericName2",
            },
        };
        var properties = new List<Property>
        {
            new()
            {
                Accessibility = Accessibility.Public,
                Descriptor = "descriptor1",
                IsBuildable = true,
                IsNullable = false,
                Name = "PropertyName1",
                Type = "string",
            },
            new()
            {
                Accessibility = Accessibility.Private,
                Descriptor = "descriptor2",
                IsBuildable = false,
                IsNullable = true,
                Name = "PropertyName2",
                Type = "int",
            },
        };
        var instance1 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = generics,
            HasDefaultConstructor = true,
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = properties,
        };
        var instance2 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = generics,
            HasDefaultConstructor = true,
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = properties,
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
        var instance1 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = new List<Generic>(),
            HasDefaultConstructor = true,
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [],
        };
        var instance2 = new Subject
        {
            Accessibility = Accessibility.Private,
            Generics = new List<Generic>(),
            HasDefaultConstructor = true,
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [],
        };

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        _ = areEqual.Should().BeFalse();
    }

    [Fact]
    public void GivenDifferentGenericsThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [new Generic { Name = "GenericName1" }],
            HasDefaultConstructor = true,
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [],
        };
        var instance2 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [new Generic { Name = "GenericName2" }],
            HasDefaultConstructor = true,
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [],
        };

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        _ = areEqual.Should().BeFalse();
    }

    [Fact]
    public void GivenDifferentHasDefaultConstructorThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            HasDefaultConstructor = true,
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [],
        };
        var instance2 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            HasDefaultConstructor = false,
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [],
        };

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        _ = areEqual.Should().BeFalse();
    }

    [Fact]
    public void GivenDifferentIsPartialThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            HasDefaultConstructor = true,
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [],
        };
        var instance2 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            HasDefaultConstructor = true,
            IsPartial = true,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [],
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
        var instance1 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            HasDefaultConstructor = true,
            IsPartial = false,
            Name = "SubjectName1",
            Namespace = "SubjectNamespace",
            Properties = [],
        };
        var instance2 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            HasDefaultConstructor = true,
            IsPartial = false,
            Name = "SubjectName2",
            Namespace = "SubjectNamespace",
            Properties = [],
        };

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        _ = areEqual.Should().BeFalse();
    }

    [Fact]
    public void GivenDifferentNamespaceThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            HasDefaultConstructor = true,
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "Namespace1",
            Properties = [],
        };
        var instance2 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            HasDefaultConstructor = true,
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "Namespace2",
            Properties = [],
        };

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        _ = areEqual.Should().BeFalse();
    }

    [Fact]
    public void GivenDifferentPropertiesThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            HasDefaultConstructor = true,
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [new Property { Name = "PropertyName1" }],
        };
        var instance2 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            HasDefaultConstructor = true,
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [new Property { Name = "PropertyName2" }],
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
        var instance = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            HasDefaultConstructor = true,
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [],
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
        Subject? instance1 = default;
        Subject? instance2 = default;

        // Act
        bool areEqual = AreEqual(instance1, instance2);

        // Assert
        _ = areEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenSameReferenceThenTheyAreEqual()
    {
        // Arrange
        var instance = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            HasDefaultConstructor = true,
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [],
        };

        // Act
        bool areEqual = AreEqual(instance, instance);

        // Assert
        _ = areEqual.Should().BeTrue();
    }

    private protected abstract bool AreEqual(Subject? instance1, Subject? instance2);
}