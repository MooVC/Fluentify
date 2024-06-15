namespace Fluentify.Syntax.TypeDeclarationSyntaxExtensionsTests;

using Fluentify.Model;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Type = Fluentify.Type;

public sealed class WhenToSubjectIsCalled
{
    public static readonly TheoryData<Compilation, bool, Type> GivenACrossReferencedTypeThenTheExpectedSubjectIsReturnedData = new()
    {
        { Classes.Instance.Compilation, true, Classes.Instance.CrossReferenced },
        { Records.Instance.Compilation, false, Records.Instance.CrossReferenced },
    };

    public static readonly TheoryData<Compilation, bool, Type> GivenAMultipleGenericTypeThenTheExpectedSubjectIsReturnedData = new()
    {
        { Classes.Instance.Compilation, true, Classes.Instance.MultipleGenerics },
        { Records.Instance.Compilation, false, Records.Instance.MultipleGenerics },
    };

    public static readonly TheoryData<Compilation, bool, Type> GivenASimpleTypeThenTheExpectedSubjectIsReturnedData = new()
    {
        { Classes.Instance.Compilation, true, Classes.Instance.Simple },
        { Records.Instance.Compilation, false, Records.Instance.Simple },
    };

    public static readonly TheoryData<Compilation, bool, Type> GivenASingleGenericTypeThenTheExpectedSubjectIsReturnedData = new()
    {
        { Classes.Instance.Compilation, true, Classes.Instance.SingleGeneric },
        { Records.Instance.Compilation, false, Records.Instance.SingleGeneric },
    };

    public static readonly TheoryData<Compilation, Type> GivenUnannotatedOrUnsupportedThenNoSubjectIsReturnedData = new()
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
    [MemberData(nameof(GivenACrossReferencedTypeThenTheExpectedSubjectIsReturnedData))]
    public void GivenACrossReferencedTypeThenTheExpectedSubjectIsReturned(Compilation compilation, bool hasDefaultConstructor, Type type)
    {
        // Arrange
        var expected = new Subject
        {
            Accessibility = Accessibility.Internal,
            HasDefaultConstructor = hasDefaultConstructor,
            Name = "CrossReferenced",
            Namespace = "Fluentify.Tests.Compilation",
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
                            Name = "global::Fluentify.Tests.Compilation.Simple",
                        },
                    },
                    Name = "Simple",
                },
            ],
        };

        // Act
        var actual = type.Syntax.ToSubject(compilation, CancellationToken.None);

        // Assert
        _ = actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(GivenAMultipleGenericTypeThenTheExpectedSubjectIsReturnedData))]
    public void GivenAMultipleGenericTypeThenTheExpectedSubjectIsReturned(Compilation compilation, bool hasDefaultConstructor, Type type)
    {
        // Arrange
        var expected = new Subject
        {
            Accessibility = Accessibility.Internal,
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
            HasDefaultConstructor = hasDefaultConstructor,
            Name = "MultipleGenerics",
            Namespace = "Fluentify.Tests.Compilation",
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
                            IsNullable = true,
                            Name = "T1?",
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
                            IsNullable = true,
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
        };

        // Act
        var actual = type.Syntax.ToSubject(compilation, CancellationToken.None);

        // Assert
        _ = actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(GivenASimpleTypeThenTheExpectedSubjectIsReturnedData))]
    public void GivenASimpleTypeThenTheExpectedSubjectIsReturned(Compilation compilation, bool hasDefaultConstructor, Type type)
    {
        // Arrange
        var expected = new Subject
        {
            Accessibility = Accessibility.Internal,
            HasDefaultConstructor = hasDefaultConstructor,
            IsPartial = true,
            Name = "Simple",
            Namespace = "Fluentify.Tests.Compilation",
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
                            IsNullable = true,
                            Name = "IReadOnlyList<object>?",
                        },
                    },
                    Name = "Attributes",
                },
            ],
        };

        // Act
        var actual = type.Syntax.ToSubject(compilation, CancellationToken.None);

        // Assert
        _ = actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(GivenASingleGenericTypeThenTheExpectedSubjectIsReturnedData))]
    public void GivenASingleGenericTypeThenTheExpectedSubjectIsReturned(Compilation compilation, bool hasDefaultConstructor, Type type)
    {
        // Arrange
        var expected = new Subject
        {
            Accessibility = Accessibility.Internal,
            Generics =
            [
                new Generic
                {
                    Constraints = ["IEnumerable"],
                    Name = "T",
                },
            ],
            HasDefaultConstructor = hasDefaultConstructor,
            Name = "SingleGeneric",
            Namespace = "Fluentify.Tests.Compilation",
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
                            IsNullable = true,
                            Name = "T",
                        },
                    },
                    Name = "Attributes",
                },
            ],
        };

        // Act
        var actual = type.Syntax.ToSubject(compilation, CancellationToken.None);

        // Assert
        _ = actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(GivenUnannotatedOrUnsupportedThenNoSubjectIsReturnedData))]
    public void GivenUnannotatedOrUnsupportedThenNoSubjectIsReturned(Compilation compilation, Type type)
    {
        // Act
        var subject = type.Syntax.ToSubject(compilation, CancellationToken.None);

        // Assert
        _ = subject.Should().BeNull();
    }
}