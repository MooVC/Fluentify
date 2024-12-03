namespace Fluentify.Syntax.TypeDeclarationSyntaxExtensionsTests;

using Fluentify.Model;
using Fluentify.Semantics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public sealed class WhenToSubjectIsCalled
{
    public static readonly TheoryData<Compilation, Definition, bool, string> GivenABooleanTypeThenTheExpectedSubjectIsReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.Boolean, false, nameof(Classes) },
        { Records.Instance.Compilation, Records.Instance.Boolean, true, nameof(Records) },
    };

    public static readonly TheoryData<Compilation, Definition, bool, string> GivenACrossReferencedTypeThenTheExpectedSubjectIsReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.CrossReferenced, false, nameof(Classes) },
        { Records.Instance.Compilation, Records.Instance.CrossReferenced, true, nameof(Records) },
    };

    public static readonly TheoryData<Compilation, Definition, bool> GivenAGlobalTypeThenTheExpectedSubjectIsReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.Global, false },
        { Records.Instance.Compilation, Records.Instance.Global, true },
    };

    public static readonly TheoryData<Compilation, Definition, bool, string> GivenAMultipleGenericTypeThenTheExpectedSubjectIsReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.MultipleGenerics, false, nameof(Classes) },
        { Records.Instance.Compilation, Records.Instance.MultipleGenerics, true, nameof(Records) },
    };

    public static readonly TheoryData<Compilation, Definition, bool, string> GivenANestedTypeThenTheExpectedSubjectIsReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.NestedInClass, false, nameof(Classes) },
        { Records.Instance.Compilation, Records.Instance.NestedInClass, true, nameof(Records) },
    };

    public static readonly TheoryData<Compilation, Definition, bool, string> GivenASimpleTypeThenTheExpectedSubjectIsReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.Simple, false, nameof(Classes) },
        { Records.Instance.Compilation, Records.Instance.Simple, true, nameof(Records) },
    };

    public static readonly TheoryData<Compilation, Definition, bool, string> GivenASingleGenericTypeThenTheExpectedSubjectIsReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.SingleGeneric, false, nameof(Classes) },
        { Records.Instance.Compilation, Records.Instance.SingleGeneric, true, nameof(Records) },
    };

    public static readonly TheoryData<Compilation, Definition> GivenUnannotatedOrUnsupportedThenNoSubjectIsReturnedData = new()
    {
        { Classes.Instance.Compilation, Classes.Instance.Unannotated },
        { Records.Instance.Compilation, Records.Instance.Unannotated },
        { Classes.Instance.Compilation, Classes.Instance.Unsupported },
        { Records.Instance.Compilation, Records.Instance.Unsupported },
    };

    [Fact]
    public void GivenNullSyntaxThenNoSubjectIsReturned()
    {
        // Arrange
        TypeDeclarationSyntax? syntax = default;

        // Act
        var subject = syntax.ToSubject(Classes.Instance.Compilation, CancellationToken.None);

        // Assert
        _ = subject.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(GivenABooleanTypeThenTheExpectedSubjectIsReturnedData))]
    public void GivenABooleanTypeThenTheExpectedSubjectIsReturned(Compilation compilation, Definition definition, bool isPartial, string type)
    {
        // Arrange
        var expected = new Subject
        {
            Accessibility = Accessibility.Public,
            IsPartial = isPartial,
            Name = "Boolean",
            Namespace = $"Fluentify.{type}.Testing",
            Properties =
            [
                new Property
                {
                    Descriptor = "WithAge",
                    Kind = new()
                    {
                        Type = new()
                        {
                            Name = "int",
                        },
                    },
                    Name = "Age",
                },
                new Property
                {
                    Descriptor = "IsRetired",
                    Kind = new()
                    {
                        Type = new()
                        {
                            Name = "bool",
                        },
                    },
                    Name = "IsRetired",
                },
                new Property
                {
                    Descriptor = "WithName",
                    Kind = new()
                    {
                        Type = new()
                        {
                            Name = "string",
                        },
                    },
                    Name = "Name",
                },
            ],
            Type = new()
            {
                IsBuildable = !isPartial,
                Name = $"global::Fluentify.{type}.Testing.Boolean",
            },
        };

        // Act
        var actual = definition.Syntax.ToSubject(compilation, CancellationToken.None);

        // Assert
        _ = actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(GivenACrossReferencedTypeThenTheExpectedSubjectIsReturnedData))]
    public void GivenACrossReferencedTypeThenTheExpectedSubjectIsReturned(Compilation compilation, Definition definition, bool isPartial, string type)
    {
        // Arrange
        var expected = new Subject
        {
            Accessibility = Accessibility.Public,
            IsPartial = isPartial,
            Name = "CrossReferenced",
            Namespace = $"Fluentify.{type}.Testing",
            Properties =
            [
                new Property
                {
                    Descriptor = "WithDescription",
                    Kind = new()
                    {
                        Type = new()
                        {
                            Name = "string",
                        },
                    },
                    Name = "Description",
                },
                new Property
                {
                    Descriptor = "WithSimple",
                    Kind = new()
                    {
                        Type = new()
                        {
                            IsBuildable = true,
                            Name = $"global::Fluentify.{type}.Testing.Simple",
                        },
                    },
                    Name = "Simple",
                },
            ],
            Type = new()
            {
                IsBuildable = !isPartial,
                Name = $"global::Fluentify.{type}.Testing.CrossReferenced",
            },
        };

        // Act
        var actual = definition.Syntax.ToSubject(compilation, CancellationToken.None);

        // Assert
        _ = actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(GivenAGlobalTypeThenTheExpectedSubjectIsReturnedData))]
    public void GivenAGlobalTypeThenTheExpectedSubjectIsReturned(Compilation compilation, Definition definition, bool isPartial)
    {
        // Arrange
        string annotation = isPartial
            ? "?"
            : string.Empty;

        var expected = new Subject
        {
            Accessibility = Accessibility.Public,
            IsPartial = isPartial,
            Name = "Global",
            Namespace = string.Empty,
            Properties =
            [
                new Property
                {
                    Descriptor = "WithAge",
                    Kind = new()
                    {
                        Type = new()
                        {
                            Name = "int",
                        },
                    },
                    Name = "Age",
                },
                new Property
                {
                    Descriptor = "WithName",
                    Kind = new()
                    {
                        Type = new()
                        {
                            Name = "string",
                        },
                    },
                    Name = "Name",
                },
                new Property
                {
                    Descriptor = "WithAttributes",
                    Kind = new()
                    {
                        Type = new()
                        {
                            IsNullable = isPartial,
                            Name = $"IReadOnlyList<object>{annotation}",
                        },
                    },
                    Name = "Attributes",
                },
            ],
            Type = new()
            {
                IsBuildable = !isPartial,
                Name = $"global::Global",
            },
        };

        // Act
        var actual = definition.Syntax.ToSubject(compilation, CancellationToken.None);

        // Assert
        _ = actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(GivenAMultipleGenericTypeThenTheExpectedSubjectIsReturnedData))]
    public void GivenAMultipleGenericTypeThenTheExpectedSubjectIsReturned(Compilation compilation, Definition definition, bool isPartial, string type)
    {
        // Arrange
        string annotation = isPartial
            ? "?"
            : string.Empty;

        var expected = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics =
            [
                new Generic
                {
                    Constraints = ["struct"],
                    Name = "T1",
                },
                new Generic
                {
                    Constraints = ["class", "new()"],
                    Name = "T2",
                },
                new Generic
                {
                    Constraints = ["IEnumerable<string>"],
                    Name = "T3",
                },
            ],
            IsPartial = isPartial,
            Name = "MultipleGenerics",
            Namespace = $"Fluentify.{type}.Testing",
            Properties =
            [
                new Property
                {
                    Descriptor = "WithAge",
                    Name = "Age",
                    Kind = new()
                    {
                        Type = new()
                        {
                            IsNullable = isPartial,
                            Name = $"T1{annotation}",
                        },
                    },
                },
                new Property
                {
                    Descriptor = "WithName",
                    Kind = new()
                    {
                        Type = new()
                        {
                            IsBuildable = true,
                            IsNullable = isPartial,
                            Name = "T2",
                        },
                    },
                    Name = "Name",
                },
                new Property
                {
                    Descriptor = "WithAttributes",
                    Kind = new()
                    {
                        Type = new()
                        {
                            Name = "T3",
                        },
                    },
                    Name = "Attributes",
                },
            ],
            Type = new()
            {
                IsBuildable = !isPartial,
                Name = $"global::Fluentify.{type}.Testing.MultipleGenerics<T1, T2, T3>",
            },
        };

        // Act
        var actual = definition.Syntax.ToSubject(compilation, CancellationToken.None);

        // Assert
        _ = actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(GivenANestedTypeThenTheExpectedSubjectIsReturnedData))]
    public void GivenANestedTypeThenTheExpectedSubjectIsReturned(Compilation compilation, Definition definition, bool isPartial, string type)
    {
        // Arrange
        string annotation = isPartial
            ? "?"
            : string.Empty;

        var expected = new Subject
        {
            Accessibility = Accessibility.Public,
            IsPartial = isPartial,
            Name = "NestedInClass",
            Namespace = $"Fluentify.{type}.Testing",
            Nesting = [new Nesting { Declaration = "partial class", Name = "Outter", Qualification = "Outter" }],
            Properties =
            [
                new Property
                {
                    Descriptor = "WithAge",
                    Kind = new()
                    {
                        Type = new()
                        {
                            Name = "int",
                        },
                    },
                    Name = "Age",
                },
                new Property
                {
                    Descriptor = "WithName",
                    Kind = new()
                    {
                        Type = new()
                        {
                            Name = "string",
                        },
                    },
                    Name = "Name",
                },
                new Property
                {
                    Descriptor = "WithAttributes",
                    Kind = new()
                    {
                        Type = new()
                        {
                            IsNullable = isPartial,
                            Name = $"IReadOnlyList<object>{annotation}",
                        },
                    },
                    Name = "Attributes",
                },
            ],
            Type = new()
            {
                IsBuildable = !isPartial,
                Name = $"global::Fluentify.{type}.Testing.Outter.NestedInClass",
            },
        };

        // Act
        var actual = definition.Syntax.ToSubject(compilation, CancellationToken.None);

        // Assert
        _ = actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(GivenASimpleTypeThenTheExpectedSubjectIsReturnedData))]
    public void GivenASimpleTypeThenTheExpectedSubjectIsReturned(Compilation compilation, Definition definition, bool isPartial, string type)
    {
        // Arrange
        string annotation = isPartial
            ? "?"
            : string.Empty;

        var expected = new Subject
        {
            Accessibility = Accessibility.Public,
            IsPartial = isPartial,
            Name = "Simple",
            Namespace = $"Fluentify.{type}.Testing",
            Properties =
            [
                new Property
                {
                    Descriptor = "WithAge",
                    Kind = new()
                    {
                        Type = new()
                        {
                            Name = "int",
                        },
                    },
                    Name = "Age",
                },
                new Property
                {
                    Descriptor = "WithName",
                    Kind = new()
                    {
                        Type = new()
                        {
                            Name = "string",
                        },
                    },
                    Name = "Name",
                },
                new Property
                {
                    Descriptor = "WithAttributes",
                    Kind = new()
                    {
                        Type = new()
                        {
                            IsNullable = isPartial,
                            Name = $"IReadOnlyList<object>{annotation}",
                        },
                    },
                    Name = "Attributes",
                },
            ],
            Type = new()
            {
                IsBuildable = !isPartial,
                Name = $"global::Fluentify.{type}.Testing.Simple",
            },
        };

        // Act
        var actual = definition.Syntax.ToSubject(compilation, CancellationToken.None);

        // Assert
        _ = actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(GivenASingleGenericTypeThenTheExpectedSubjectIsReturnedData))]
    public void GivenASingleGenericTypeThenTheExpectedSubjectIsReturned(Compilation compilation, Definition definition, bool isPartial, string type)
    {
        // Arrange
        var expected = new Subject
        {
            Accessibility = Accessibility.Public,
            Generics =
            [
                new Generic
                {
                    Constraints = ["IEnumerable"],
                    Name = "T",
                },
            ],
            IsPartial = isPartial,
            Name = "SingleGeneric",
            Namespace = $"Fluentify.{type}.Testing",
            Properties =
            [
                new Property
                {
                    Descriptor = "WithAge",
                    Kind = new()
                    {
                        Type = new()
                        {
                            Name = "int",
                        },
                    },
                    Name = "Age",
                },
                new Property
                {
                    Descriptor = "WithName",
                    Kind = new()
                    {
                        Type = new()
                        {
                            Name = "string",
                        },
                    },
                    Name = "Name",
                },
                new Property
                {
                    Descriptor = "WithAttributes",
                    Kind = new()
                    {
                        Type = new()
                        {
                            IsNullable = isPartial,
                            Name = "T",
                        },
                    },
                    Name = "Attributes",
                },
            ],
            Type = new()
            {
                IsBuildable = !isPartial,
                Name = $"global::Fluentify.{type}.Testing.SingleGeneric<T>",
            },
        };

        // Act
        var actual = definition.Syntax.ToSubject(compilation, CancellationToken.None);

        // Assert
        _ = actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(GivenUnannotatedOrUnsupportedThenNoSubjectIsReturnedData))]
    public void GivenUnannotatedOrUnsupportedThenNoSubjectIsReturned(Compilation compilation, Definition definition)
    {
        // Act
        var subject = definition.Syntax.ToSubject(compilation, CancellationToken.None);

        // Assert
        _ = subject.Should().BeNull();
    }
}