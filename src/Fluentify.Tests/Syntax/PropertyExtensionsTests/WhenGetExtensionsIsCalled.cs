namespace Fluentify.Syntax.PropertyExtensionsTests;

using Fluentify.Model;
using Microsoft.CodeAnalysis;

public sealed partial class WhenGetExtensionsIsCalled
{
    [Test]
    public void GivenAScalarThenYieldsNullThenNoExtensionIsGenerated()
    {
        // Arrange
        var subject = new Subject
        {
            Accessibility = Accessibility.Public,
            Name = "TestSubject",
            Properties = [],
            Type = new()
            {
                Name = "global::TestSubject",
            },
        };

        var property = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "WithTestProperty",
            Kind = new()
            {
                Type = new()
                {
                    IsFrameworkType = true,
                    IsValueType = true,
                    Name = "int",
                },
            },
            Name = "TestProperty",
        };

        // Act
        string result = property.GetExtensions(subject, _ => default).ToString();

        // Assert
        result.ShouldBeEmpty();
    }

    [Test]
    [Arguments("throw new NotImplementedException();")]
    [Arguments("return new();")]
    public void GivenBuildablePropertyThenGeneratesDelegateExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            public static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(this global::TestSubject subject, TestType instance, global::System.Func<TestType, TestType> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithTestProperty(instance);
                }

                public static global::TestSubject WithTestProperty(this global::TestSubject subject, global::System.Func<TestType, TestType> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = subject.TestProperty;

                    if (global::System.Object.ReferenceEquals(instance, null))
                    {
                        instance = new TestType();
                    }

                    instance = builder(instance);

                    return subject.WithTestProperty(instance);
                }

                public static global::TestSubject WithTestProperty(this global::TestSubject subject, TestType value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    {{scalar}}
                }
            }
            """;

        var subject = new Subject
        {
            Accessibility = Accessibility.Public,
            Name = "TestSubject",
            Properties = [],
            Type = new()
            {
                Initialization = "new global::TestSubject()",
                Name = "global::TestSubject",
            },
        };

        var property = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "WithTestProperty",
            Kind = new()
            {
                Type = new()
                {
                    Initialization = "new TestType()",
                    IsBuildable = true,
                    Name = "TestType",
                },
            },
            Name = "TestProperty",
        };

        // Act
        string result = property.GetExtensions(subject, _ => scalar).ToString();

        // Assert
        result.ShouldBe(expected);
    }

    [Test]
    public void GivenFrameworkTypeThenDelegateExtensionIsNotGenerated()
    {
        // Arrange
        const string Scalar = "throw new NotImplementedException();";

        string expected = $$"""
            public static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(this global::TestSubject subject, string value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    {{Scalar}}
                }
            }
            """;

        var subject = new Subject
        {
            Accessibility = Accessibility.Public,
            Name = "TestSubject",
            Properties = [],
            Type = new()
            {
                Name = "global::TestSubject",
            },
        };

        var property = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "WithTestProperty",
            Kind = new()
            {
                Type = new()
                {
                    IsFrameworkType = true,
                    Name = "string",
                },
            },
            Name = "TestProperty",
        };

        // Act
        string result = property.GetExtensions(subject, _ => Scalar).ToString();

        // Assert
        result.ShouldBe(expected);
    }

    [Test]
    [Arguments("throw new NotImplementedException();")]
    [Arguments("return new();")]
    public void GivenInternalPropertyAndPublicSubjectThenGeneratesInternalExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            public static partial class TestSubjectExtensions
            {
                internal static global::TestSubject WithTestProperty(this global::TestSubject subject, int value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    {{scalar}}
                }
            }
            """;

        var subject = new Subject
        {
            Accessibility = Accessibility.Public,
            Name = "TestSubject",
            Properties = [],
            Type = new()
            {
                Name = "global::TestSubject",
            },
        };

        var property = new Property
        {
            Accessibility = Accessibility.Internal,
            Descriptor = "WithTestProperty",
            Kind = new()
            {
                Type = new()
                {
                    IsFrameworkType = true,
                    IsValueType = true,
                    Name = "int",
                },
            },
            Name = "TestProperty",
        };

        // Act
        string result = property.GetExtensions(subject, _ => scalar).ToString();

        // Assert
        result.ShouldBe(expected);
    }

    [Test]
    public void GivenNonBuildablePropertyThenGeneratesDelegateUsingExistingInstance()
    {
        // Arrange
        const string Scalar = "throw new NotImplementedException();";

        string expected = $$"""
            public static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(this global::TestSubject subject, TestType instance, global::System.Func<TestType, TestType> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithTestProperty(instance);
                }

                public static global::TestSubject WithTestProperty(this global::TestSubject subject, global::System.Func<TestType, TestType> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = subject.TestProperty;

                    if (global::System.Object.ReferenceEquals(instance, null))
                    {
                        throw new global::System.NotSupportedException();
                    }

                    instance = builder(instance);

                    return subject.WithTestProperty(instance);
                }

                public static global::TestSubject WithTestProperty(this global::TestSubject subject, TestType value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    {{Scalar}}
                }
            }
            """;

        var subject = new Subject
        {
            Accessibility = Accessibility.Public,
            Name = "TestSubject",
            Properties = [],
            Type = new()
            {
                Name = "global::TestSubject",
            },
        };

        var property = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "WithTestProperty",
            Kind = new()
            {
                Type = new()
                {
                    IsBuildable = false,
                    Name = "TestType",
                },
            },
            Name = "TestProperty",
        };

        // Act
        string result = property.GetExtensions(subject, _ => Scalar).ToString();

        // Assert
        result.ShouldBe(expected);
    }

    [Test]
    [Arguments("throw new NotImplementedException();")]
    [Arguments("return new();")]
    public void GivenNullablePropertyThenGeneratesExtensionWithNullableType(string scalar)
    {
        string expected = $$"""
            public static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(this global::TestSubject subject, int? value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    {{scalar}}
                }
            }
            """;

        // Arrange
        var subject = new Subject
        {
            Accessibility = Accessibility.Public,
            Name = "TestSubject",
            Properties = [],
            SupportsNullableReferenceTypes = true,
            Type = new()
            {
                Name = "global::TestSubject",
            },
        };

        var property = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "WithTestProperty",
            Kind = new()
            {
                Type = new()
                {
                    IsFrameworkType = true,
                    IsNullable = true,
                    IsValueType = true,
                    Name = "int",
                },
            },
            Name = "TestProperty",
        };

        // Act
        string result = property.GetExtensions(subject, _ => scalar).ToString();

        // Assert
        result.ShouldBe(expected);
    }

    [Test]
    [Arguments("throw new NotImplementedException();")]
    [Arguments("return new();")]
    public void GivenPublicPropertyAndPublicSubjectThenGeneratesPublicExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            public static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(this global::TestSubject subject, int value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    {{scalar}}
                }
            }
            """;

        var subject = new Subject
        {
            Accessibility = Accessibility.Public,
            Name = "TestSubject",
            Properties = [],
            Type = new()
            {
                Name = "global::TestSubject",
            },
        };

        var property = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "WithTestProperty",
            Kind = new()
            {
                Type = new()
                {
                    IsFrameworkType = true,
                    IsValueType = true,
                    Name = "int",
                },
            },
            Name = "TestProperty",
        };

        // Act
        string result = property.GetExtensions(subject, _ => scalar).ToString();

        // Assert
        result.ShouldBe(expected);
    }
}