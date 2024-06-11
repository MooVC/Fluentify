namespace Fluentify.Syntax.TypeDeclarationSyntaxExtensionsTests;

using Fluentify.Model;
using Microsoft.CodeAnalysis;
using Type = Fluentify.Type;

public sealed class WhenToSubjectIsCalled
{
    public static readonly TheoryData<Compilation, bool, Type> GivenACrossReferencedTypeTheTheExpectedSubjectIsReturnedData = new()
    {
        { Classes.Instance.Compilation, true, Classes.Instance.CrossReferenced },
        { Records.Instance.Compilation, false, Records.Instance.CrossReferenced },
    };

    public static readonly TheoryData<Compilation, bool, Type> GivenAMultipleGenericTypeTheTheExpectedSubjectIsReturnedData = new()
    {
        { Classes.Instance.Compilation, true, Classes.Instance.MultipleGenerics },
        { Records.Instance.Compilation, false, Records.Instance.MultipleGenerics },
    };

    public static readonly TheoryData<Compilation, bool, Type> GivenASimpleTypeTheTheExpectedSubjectIsReturnedData = new()
    {
        { Classes.Instance.Compilation, true, Classes.Instance.Simple },
        { Records.Instance.Compilation, false, Records.Instance.Simple },
    };

    public static readonly TheoryData<Compilation, bool, Type> GivenASingleGenericTypeTheTheExpectedSubjectIsReturnedData = new()
    {
        { Classes.Instance.Compilation, true, Classes.Instance.SingleGeneric },
        { Records.Instance.Compilation, false, Records.Instance.SingleGeneric },
    };

    [Theory]
    [MemberData(nameof(GivenACrossReferencedTypeTheTheExpectedSubjectIsReturnedData))]
    public void GivenACrossReferencedTypeTheTheExpectedSubjectIsReturned(Compilation compilation, bool hasDefaultConstructor, Type type)
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
    [MemberData(nameof(GivenAMultipleGenericTypeTheTheExpectedSubjectIsReturnedData))]
    public void GivenAMultipleGenericTypeTheTheExpectedSubjectIsReturned(Compilation compilation, bool hasDefaultConstructor, Type type)
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
    [MemberData(nameof(GivenASimpleTypeTheTheExpectedSubjectIsReturnedData))]
    public void GivenASimpleTypeTheTheExpectedSubjectIsReturned(Compilation compilation, bool hasDefaultConstructor, Type type)
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
    [MemberData(nameof(GivenASingleGenericTypeTheTheExpectedSubjectIsReturnedData))]
    public void GivenASingleGenericTypeTheTheExpectedSubjectIsReturned(Compilation compilation, bool hasDefaultConstructor, Type type)
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
}