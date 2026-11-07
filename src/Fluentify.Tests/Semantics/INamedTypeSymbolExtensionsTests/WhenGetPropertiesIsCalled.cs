namespace Fluentify.Semantics.INamedTypeSymbolExtensionsTests;

using Fluentify.Model;
using Fluentify.Semantics;
using Microsoft.CodeAnalysis;

public sealed class WhenGetPropertiesIsCalled
{
    private static readonly Property _age = new()
    {
        Descriptor = "WithAge",
        Kind = new()
        {
            Type = new()
            {
                Initialization = "new int()",
                IsFrameworkType = true,
                Name = "int",
            },
        },
        Name = "Age",
    };

    private static readonly Property _name = new()
    {
        Descriptor = "WithName",
        Kind = new()
        {
            Type = new()
            {
                Initialization = "new string()",
                IsFrameworkType = true,
                Name = "string",
            },
        },
        Name = "Name",
    };

    public static IEnumerable<(Compilation Compilation, Definition Definition)> GivenAllThreeIgnoredThenTheExpectedPropertiesAreReturnedData()
    {
        return
        [
            (Classes.Instance.Compilation, Classes.Instance.AllThreeIgnored),
            (Records.Instance.Compilation, Records.Instance.AllThreeIgnored),
        ];
    }

    public static IEnumerable<(Compilation Compilation, Definition Definition)> GivenBooleanThenTheExpectedPropertiesAreReturnedData()
    {
        return
        [
            (Classes.Instance.Compilation, Classes.Instance.Boolean),
            (Records.Instance.Compilation, Records.Instance.Boolean),
        ];
    }

    public static IEnumerable<(Compilation Compilation, Definition Definition, string Type)> GivenCrossReferencedThenTheExpectedPropertiesAreReturnedData()
    {
        return
        [
            (Classes.Instance.Compilation, Classes.Instance.CrossReferenced, nameof(Classes)),
            (Records.Instance.Compilation, Records.Instance.CrossReferenced, nameof(Records)),
        ];
    }

    public static IEnumerable<(Compilation Compilation, Definition Definition, bool IsNullable)> GivenDescriptorOnRequiredThenTheExpectedPropertiesAreReturnedData()
    {
        return
        [
            (Classes.Instance.Compilation, Classes.Instance.DescriptorOnRequired, false),
            (Records.Instance.Compilation, Records.Instance.DescriptorOnRequired, true),
        ];
    }

    public static IEnumerable<(Compilation Compilation, Definition Definition, bool IsNullable)> GivenDescriptorOnOptionalThenTheExpectedPropertiesAreReturnedData()
    {
        return
        [
            (Classes.Instance.Compilation, Classes.Instance.DescriptorOnOptional, false),
            (Records.Instance.Compilation, Records.Instance.DescriptorOnOptional, true),
        ];
    }

    public static IEnumerable<(Compilation Compilation, Definition Definition, bool IsNullable)> GivenDescriptorOnIgnoredThenTheExpectedPropertiesAreReturnedData()
    {
        return
        [
            (Classes.Instance.Compilation, Classes.Instance.DescriptorOnIgnored, false),
            (Records.Instance.Compilation, Records.Instance.DescriptorOnIgnored, true),
        ];
    }

    public static IEnumerable<(Compilation Compilation, Definition Definition, bool IsNullable)> GivenGlobalThenTheExpectedPropertiesAreReturnedData()
    {
        return
        [
            (Classes.Instance.Compilation, Classes.Instance.Global, false),
            (Records.Instance.Compilation, Records.Instance.Global, true),
        ];
    }

    public static IEnumerable<(Compilation Compilation, Definition Definition, bool IsNullable)> GivenMultipleGenericsThenTheExpectedPropertiesAreReturnedData()
    {
        return
        [
            (Classes.Instance.Compilation, Classes.Instance.MultipleGenerics, false),
            (Records.Instance.Compilation, Records.Instance.MultipleGenerics, true),
        ];
    }

    public static IEnumerable<(Compilation Compilation, Definition Definition, bool IsNullable)> GivenOneOfThreeIgnoredThenTheExpectedPropertiesAreReturnedData()
    {
        return
        [
            (Classes.Instance.Compilation, Classes.Instance.OneOfThreeIgnored, false),
            (Records.Instance.Compilation, Records.Instance.OneOfThreeIgnored, true),
        ];
    }

    public static IEnumerable<(Compilation Compilation, Definition Definition, bool IsNullable)> GivenSimpleThenTheExpectedPropertiesAreReturnedData()
    {
        return
        [
            (Classes.Instance.Compilation, Classes.Instance.Simple, false),
            (Records.Instance.Compilation, Records.Instance.Simple, true),
        ];
    }

    public static IEnumerable<(Compilation Compilation, Definition Definition, bool IsNullable)> GivenSingleGenericThenTheExpectedPropertiesAreReturnedData()
    {
        return
        [
            (Classes.Instance.Compilation, Classes.Instance.SingleGeneric, false),
            (Records.Instance.Compilation, Records.Instance.SingleGeneric, true),
        ];
    }

    public static IEnumerable<(Compilation Compilation, Definition Definition, bool IsNullable)> GivenTwoOfThreeIgnoredThenTheExpectedPropertiesAreReturnedData()
    {
        return
        [
            (Classes.Instance.Compilation, Classes.Instance.TwoOfThreeIgnored, false),
            (Records.Instance.Compilation, Records.Instance.TwoOfThreeIgnored, true),
        ];
    }

    [Test]
    [MethodDataSource(nameof(GivenAllThreeIgnoredThenTheExpectedPropertiesAreReturnedData))]
    public void GivenAllThreeIgnoredThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition)
    {
        // Act
        IReadOnlyList<Property> properties = definition.Symbol.GetProperties(compilation, CancellationToken.None);

        // Assert
        properties.Count.ShouldBe(3);
        properties.ShouldAllBe(property => property.IsIgnored);
    }

    [Test]
    [MethodDataSource(nameof(GivenBooleanThenTheExpectedPropertiesAreReturnedData))]
    public void GivenBooleanThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition)
    {
        // Arrange
        var isRetired = new Property
        {
            Descriptor = "IsRetired",
            Kind = new()
            {
                Type = new()
                {
                    Initialization = "new bool()",
                    IsFrameworkType = true,
                    Name = "bool",
                },
            },
            Name = "IsRetired",
        };

        // Act & Assert
        ActAndAssert(compilation, definition, _age, isRetired, _name);
    }

    [Test]
    [MethodDataSource(nameof(GivenCrossReferencedThenTheExpectedPropertiesAreReturnedData))]
    public void GivenCrossReferencedThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition, string type)
    {
        // Arrange
        var description = new Property
        {
            Descriptor = "WithDescription",
            Kind = new()
            {
                Type = new()
                {
                    Initialization = "new string()",
                    IsFrameworkType = true,
                    Name = "string",
                },
            },
            Name = "Description",
        };

        var simple = new Property
        {
            Descriptor = "WithSimple",
            Kind = new()
            {
                Type = new()
                {
                    Initialization = $"new global::Fluentify.{type}.Testing.Simple()",
                    IsBuildable = true,
                    Name = $"global::Fluentify.{type}.Testing.Simple",
                },
            },
            Name = "Simple",
        };

        // Act & Assert
        ActAndAssert(compilation, definition, description, simple);
    }

    [Test]
    [MethodDataSource(nameof(GivenDescriptorOnIgnoredThenTheExpectedPropertiesAreReturnedData))]
    public void GivenDescriptorOnIgnoredThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition, bool isNullable)
    {
        // Arrange
        var name = new Property
        {
            Descriptor = "WithName",
            IsIgnored = true,
            Kind = new()
            {
                Type = new()
                {
                    Initialization = "new string()",
                    IsFrameworkType = true,
                    Name = "string",
                },
            },
            Name = "Name",
        };

        Property attributes = GetAttributes(isNullable);

        // Act & Assert
        ActAndAssert(compilation, definition, _age, attributes, name);
    }

    [Test]
    [MethodDataSource(nameof(GivenDescriptorOnOptionalThenTheExpectedPropertiesAreReturnedData))]
    public void GivenDescriptorOnOptionalThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition, bool isNullable)
    {
        // Arrange
        Property attributes = GetAttributes(isNullable, descriptor: "AttributedWith");

        // Act & Assert
        ActAndAssert(compilation, definition, _age, attributes, _name);
    }

    [Test]
    [MethodDataSource(nameof(GivenDescriptorOnRequiredThenTheExpectedPropertiesAreReturnedData))]
    public void GivenDescriptorOnRequiredThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition, bool isNullable)
    {
        // Arrange
        var age = new Property
        {
            Descriptor = "Aged",
            Kind = new()
            {
                Type = new()
                {
                    Initialization = "new int()",
                    IsFrameworkType = true,
                    Name = "int",
                },
            },
            Name = "Age",
        };

        Property attributes = GetAttributes(isNullable);

        // Act & Assert
        ActAndAssert(compilation, definition, age, attributes, _name);
    }

    [Test]
    [MethodDataSource(nameof(GivenGlobalThenTheExpectedPropertiesAreReturnedData))]
    public void GivenGlobalThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition, bool isNullable)
    {
        GivenSimpleThenTheExpectedPropertiesAreReturned(compilation, definition, isNullable);
    }

    [Test]
    [MethodDataSource(nameof(GivenMultipleGenericsThenTheExpectedPropertiesAreReturnedData))]
    public void GivenMultipleGenericsThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition, bool isNullable)
    {
        // Arrange
        string annotation = isNullable
            ? "?"
            : string.Empty;

        var age = new Property
        {
            Descriptor = "WithAge",
            Kind = new()
            {
                Type = new()
                {
                    Initialization = "new T1()",
                    IsNullable = isNullable,
                    IsValueType = true,
                    Name = $"T1{annotation}",
                },
            },
            Name = "Age",
        };

        var attributes = new Property
        {
            Descriptor = "WithAttributes",
            Kind = new()
            {
                Type = new()
                {
                    Initialization = "new T3()",
                    Name = "T3",
                },
            },
            Name = "Attributes",
        };

        var name = new Property
        {
            Descriptor = "WithName",
            Kind = new()
            {
                Type = new()
                {
                    Initialization = "new T2()",
                    IsBuildable = true,
                    IsNullable = isNullable,
                    Name = "T2",
                },
            },
            Name = "Name",
        };

        // Act & Assert
        ActAndAssert(compilation, definition, age, attributes, name);
    }

    [Test]
    [MethodDataSource(nameof(GivenOneOfThreeIgnoredThenTheExpectedPropertiesAreReturnedData))]
    public void GivenOneOfThreeIgnoredThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition, bool isNullable)
    {
        // Arrange
        var name = new Property
        {
            Descriptor = "WithName",
            IsIgnored = true,
            Kind = new()
            {
                Type = new()
                {
                    Initialization = "new string()",
                    IsFrameworkType = true,
                    Name = "string",
                },
            },
            Name = "Name",
        };

        Property attributes = GetAttributes(isNullable);

        // Act & Assert
        ActAndAssert(compilation, definition, _age, attributes, name);
    }

    [Test]
    [MethodDataSource(nameof(GivenSimpleThenTheExpectedPropertiesAreReturnedData))]
    public void GivenSimpleThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition, bool isNullable)
    {
        // Arrange
        Property attributes = GetAttributes(isNullable);

        // Act & Assert
        ActAndAssert(compilation, definition, _age, attributes, _name);
    }

    [Test]
    [MethodDataSource(nameof(GivenSingleGenericThenTheExpectedPropertiesAreReturnedData))]
    public void GivenSingleGenericThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition, bool isNullable)
    {
        // Arrange
        var attributes = new Property
        {
            Descriptor = "WithAttributes",
            Kind = new()
            {
                Type = new()
                {
                    Initialization = "new T()",
                    IsNullable = isNullable,
                    Name = "T",
                },
            },
            Name = "Attributes",
        };

        // Act & Assert
        ActAndAssert(compilation, definition, _age, attributes, _name);
    }

    [Test]
    [MethodDataSource(nameof(GivenTwoOfThreeIgnoredThenTheExpectedPropertiesAreReturnedData))]
    public void GivenTwoOfThreeIgnoredThenTheExpectedPropertiesAreReturned(Compilation compilation, Definition definition, bool isNullable)
    {
        // Arrange
        var age = new Property
        {
            Descriptor = "WithAge",
            IsIgnored = true,
            Kind = new()
            {
                Type = new()
                {
                    Initialization = "new int()",
                    IsFrameworkType = true,
                    Name = "int",
                },
            },
            Name = "Age",
        };

        Property attributes = GetAttributes(isNullable);

        var name = new Property
        {
            Descriptor = "WithName",
            IsIgnored = true,
            Kind = new()
            {
                Type = new()
                {
                    Initialization = "new string()",
                    IsFrameworkType = true,
                    Name = "string",
                },
            },
            Name = "Name",
        };

        // Act & Assert
        ActAndAssert(compilation, definition, age, attributes, name);
    }

    private static void ActAndAssert(Compilation compilation, Definition definition, params Property[] expected)
    {
        // Act
        IReadOnlyList<Property> actual = definition.Symbol.GetProperties(compilation, CancellationToken.None);

        // Assert
        actual.Count.ShouldBe(expected.Length);
        ClearThrowsWhenValueIsNull(actual);
        actual.ShouldBeSubsetOf(expected);
    }

    private static void ClearThrowsWhenValueIsNull(IReadOnlyList<Property> properties)
    {
        foreach (Property property in properties)
        {
            property.ThrowsWhenValueIsNull = false;
        }
    }

    private static Property GetAttributes(bool isNullable, string descriptor = "WithAttributes")
    {
        string annotation = isNullable
            ? "?"
            : string.Empty;

        return new Property
        {
            Descriptor = descriptor,
            Kind = new()
            {
                Type = new()
                {
                    Initialization = "new IReadOnlyList<object>()",
                    IsNullable = isNullable,
                    Name = $"IReadOnlyList<object>{annotation}",
                },
            },
            Name = "Attributes",
        };
    }
}