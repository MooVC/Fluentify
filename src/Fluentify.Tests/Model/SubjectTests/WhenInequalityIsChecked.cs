namespace Fluentify.Model.SubjectTests;

using System.Collections.Generic;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Xunit;

public abstract class WhenInequalityIsChecked
{
    [Fact]
    public void GivenIdenticalInstancesThenTheyAreNotEqual()
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
                Kind = new()
                {
                    Type = new()
                    {
                        Name = "string",
                    },
                },
                Name = "PropertyName1",
            },
            new()
            {
                Accessibility = Accessibility.Private,
                Descriptor = "descriptor2",
                Kind = new()
                {
                    Type = new()
                    {
                        IsNullable = true,
                        Name = "int",
                    },
                },
                Name = "PropertyName2",
            },
        };
        var instance1 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = generics,
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = properties,
            Type = new()
            {
                IsBuildable = true,
                Name = "SubjectNamespace.SubjectName",
            },
        };
        var instance2 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = generics,
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = properties,
            Type = new()
            {
                IsBuildable = true,
                Name = "SubjectNamespace.SubjectName",
            },
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
        var instance1 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [],
            Type = new()
            {
                IsBuildable = true,
                Name = "SubjectNamespace.SubjectName",
            },
        };
        var instance2 = new Subject
        {
            Accessibility = Accessibility.Private,
            Generics = [],
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [],
            Type = new()
            {
                IsBuildable = true,
                Name = "SubjectNamespace.SubjectName",
            },
        };

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        _ = areNotEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentGenericsThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [new Generic { Name = "GenericName1" }],
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [],
            Type = new()
            {
                IsBuildable = true,
                Name = "SubjectNamespace.SubjectName",
            },
        };
        var instance2 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [new Generic { Name = "GenericName2" }],
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [],
            Type = new()
            {
                IsBuildable = true,
                Name = "SubjectNamespace.SubjectName",
            },
        };

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        _ = areNotEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentIsPartialThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [],
            Type = new()
            {
                IsBuildable = true,
                Name = "SubjectNamespace.SubjectName",
            },
        };
        var instance2 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            IsPartial = true,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [],
            Type = new()
            {
                IsBuildable = true,
                Name = "SubjectNamespace.SubjectName",
            },
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
        var instance1 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            IsPartial = false,
            Name = "SubjectName1",
            Namespace = "SubjectNamespace",
            Properties = [],
            Type = new()
            {
                IsBuildable = true,
                Name = "SubjectNamespace.SubjectName",
            },
        };
        var instance2 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            IsPartial = false,
            Name = "SubjectName2",
            Namespace = "SubjectNamespace",
            Properties = [],
            Type = new()
            {
                IsBuildable = true,
                Name = "SubjectNamespace.SubjectName",
            },
        };

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        _ = areNotEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentNamespaceThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "Namespace1",
            Properties = [],
            Type = new()
            {
                IsBuildable = true,
                Name = "SubjectNamespace.SubjectName",
            },
        };
        var instance2 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "Namespace2",
            Properties = [],
            Type = new()
            {
                IsBuildable = true,
                Name = "SubjectNamespace.SubjectName",
            },
        };

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        _ = areNotEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentPropertiesThenTheyAreNotEqual()
    {
        // Arrange
        var instance1 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [new Property { Name = "PropertyName1" }],
            Type = new()
            {
                IsBuildable = true,
                Name = "SubjectNamespace.SubjectName",
            },
        };
        var instance2 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [new Property { Name = "PropertyName2" }],
            Type = new()
            {
                IsBuildable = true,
                Name = "SubjectNamespace.SubjectName",
            },
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
        var instance1 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [],
            Type = new()
            {
                IsBuildable = true,
                Name = "SubjectNamespace.SubjectName",
            },
        };
        var instance2 = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [],
            Type = new()
            {
                IsBuildable = false,
                Name = "SubjectNamespace.SubjectName",
            },
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
        var instance = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [],
            Type = new()
            {
                IsBuildable = true,
                Name = "SubjectNamespace.SubjectName",
            },
        };

        // Act
        bool areNotEqual = AreNotEqual(instance, default);

        // Assert
        _ = areNotEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenBothInstancesAreNullThenTheyAreNotEqual()
    {
        // Arrange
        Subject? instance1 = default;
        Subject? instance2 = default;

        // Act
        bool areNotEqual = AreNotEqual(instance1, instance2);

        // Assert
        _ = areNotEqual.Should().BeFalse();
    }

    [Fact]
    public void GivenSameReferenceThenTheyAreNotEqual()
    {
        // Arrange
        var instance = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics = [],
            IsPartial = false,
            Name = "SubjectName",
            Namespace = "SubjectNamespace",
            Properties = [],
            Type = new()
            {
                IsBuildable = true,
                Name = "SubjectNamespace.SubjectName",
            },
        };

        // Act
        bool areNotEqual = AreNotEqual(instance, instance);

        // Assert
        _ = areNotEqual.Should().BeFalse();
    }

    private protected abstract bool AreNotEqual(Subject? instance1, Subject? instance2);
}