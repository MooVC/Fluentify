namespace Fluentify.Source.PropertyExtensionsTests;

using Fluentify.Model;
using Microsoft.CodeAnalysis;
using Metadata = Fluentify.Source.Metadata;

public sealed partial class WhenGetExtensionsIsCalled
{
    [Fact]
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

        var metadata = new Metadata
        {
            Constraints = [],
            Parameters = string.Empty,
            Subject = subject,
        };

        // Act
        string result = property.GetExtensions(ref metadata, _ => default);

        // Assert
        result.ShouldBeEmpty();
    }

    [Theory]
    [InlineData("throw new NotImplementedException();")]
    [InlineData("return new();")]
    public void GivenPublicPropertyAndPublicSubjectThenGeneratesPublicExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(
                    this global::TestSubject subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

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

        var metadata = new Metadata
        {
            Constraints = [],
            Parameters = string.Empty,
            Subject = subject,
        };

        // Act
        string result = property.GetExtensions(ref metadata, _ => scalar);

        // Assert
        result.ShouldBe(expected);
    }

    [Theory]
    [InlineData("throw new NotImplementedException();")]
    [InlineData("return new();")]
    public void GivenInternalPropertyAndPublicSubjectThenGeneratesInternalExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            internal static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(
                    this global::TestSubject subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

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

        var metadata = new Metadata
        {
            Constraints = [],
            Parameters = string.Empty,
            Subject = subject,
        };

        // Act
        string result = property.GetExtensions(ref metadata, _ => scalar);

        // Assert
        result.ShouldBe(expected);
    }

    [Theory]
    [InlineData("throw new NotImplementedException();")]
    [InlineData("return new();")]
    public void GivenNullablePropertyThenGeneratesExtensionWithNullableType(string scalar)
    {
        string expected = $$"""
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(
                    this global::TestSubject subject,
                    int? value)
                {
                    subject.ThrowIfNull("subject");

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

        var metadata = new Metadata
        {
            Constraints = [],
            Parameters = string.Empty,
            Subject = subject,
        };

        // Act
        string result = property.GetExtensions(ref metadata, _ => scalar);

        // Assert
        result.ShouldBe(expected);
    }

    [Fact]
    public void GivenFrameworkTypeThenDelegateExtensionIsNotGenerated()
    {
        // Arrange
        const string Scalar = "throw new NotImplementedException();";

        string expected = $$"""
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(
                    this global::TestSubject subject,
                    string value)
                {
                    subject.ThrowIfNull("subject");

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

        var metadata = new Metadata
        {
            Constraints = [],
            Parameters = string.Empty,
            Subject = subject,
        };

        // Act
        string result = property.GetExtensions(ref metadata, _ => Scalar);

        // Assert
        result.ShouldBe(expected);
    }

    [Theory]
    [InlineData("throw new NotImplementedException();")]
    [InlineData("return new();")]
    public void GivenBuildablePropertyThenGeneratesDelegateExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(
                    this global::TestSubject subject,
                    Func<TestType, TestType> builder)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    var instance = subject.TestProperty;

                    if (ReferenceEquals(instance, null))
                    {
                        instance = new TestType();
                    }

                    instance = builder(instance);

                    return subject.WithTestProperty(instance);
                }

                public static global::TestSubject WithTestProperty(
                    this global::TestSubject subject,
                    TestType value)
                {
                    subject.ThrowIfNull("subject");

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

        var metadata = new Metadata
        {
            Constraints = [],
            Parameters = string.Empty,
            Subject = subject,
        };

        // Act
        string result = property.GetExtensions(ref metadata, _ => scalar);

        // Assert
        result.ShouldBe(expected);
    }

    [Fact]
    public void GivenNonBuildablePropertyThenGeneratesDelegateUsingExistingInstance()
    {
        // Arrange
        const string Scalar = "throw new NotImplementedException();";

        string expected = $$"""
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(
                    this global::TestSubject subject,
                    Func<TestType, TestType> builder)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    var instance = subject.TestProperty;

                    if (ReferenceEquals(instance, null))
                    {
                        throw new NotSupportedException();
                    }

                    instance = builder(instance);

                    return subject.WithTestProperty(instance);
                }

                public static global::TestSubject WithTestProperty(
                    this global::TestSubject subject,
                    TestType value)
                {
                    subject.ThrowIfNull("subject");

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

        var metadata = new Metadata
        {
            Constraints = [],
            Parameters = string.Empty,
            Subject = subject,
        };

        // Act
        string result = property.GetExtensions(ref metadata, _ => Scalar);

        // Assert
        result.ShouldBe(expected);
    }
}