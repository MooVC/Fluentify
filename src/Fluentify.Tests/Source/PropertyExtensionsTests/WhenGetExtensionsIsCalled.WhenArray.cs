namespace Fluentify.Source.PropertyExtensionsTests;

using Fluentify.Model;
using Microsoft.CodeAnalysis;
using Metadata = Fluentify.Source.Metadata;

public sealed partial class WhenGetExtensionsIsCalled
{
    [Fact]
    public void GivenAScalarThenYieldsNullWhenArrayThenNoExtensionIsGenerated()
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
                Member = new()
                {
                    Name = "int",
                },
                Pattern = Pattern.Array,
                Type = new()
                {
                    Name = "int[]",
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
        _ = result.Should().BeEmpty();
    }

    [Theory]
    [InlineData("throw new NotImplementedException();")]
    [InlineData("return new();")]
    public void GivenPublicPropertyAndPublicSubjectWhenArrayThenGeneratesPublicExtension(string scalar)
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
                    params int[] values)
                {
                    subject.ThrowIfNull("subject");

                    int[] value = values;

                    if (subject.TestProperty != null)
                    {
                        value = new int[value.Length + subject.TestProperty.Length];

                        subject.TestProperty.CopyTo(value, 0);
                        values.CopyTo(value, subject.TestProperty.Length);
                    }

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
                Member = new()
                {
                    Name = "int",
                },
                Pattern = Pattern.Array,
                Type = new()
                {
                    Name = "int[]",
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
        _ = result.Should().Be(expected);
    }

    [Theory]
    [InlineData("throw new NotImplementedException();")]
    [InlineData("return new();")]
    public void GivenInternalPropertyAndPublicSubjectWhenArrayThenGeneratesInternalExtension(string scalar)
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
                    params int[] values)
                {
                    subject.ThrowIfNull("subject");

                    int[] value = values;

                    if (subject.TestProperty != null)
                    {
                        value = new int[value.Length + subject.TestProperty.Length];

                        subject.TestProperty.CopyTo(value, 0);
                        values.CopyTo(value, subject.TestProperty.Length);
                    }

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
                Member = new()
                {
                    Name = "int",
                },
                Pattern = Pattern.Array,
                Type = new()
                {
                    Name = "int[]",
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
        _ = result.Should().Be(expected);
    }

    [Theory]
    [InlineData("throw new NotImplementedException();")]
    [InlineData("return new();")]
    public void GivenNullablePropertyWhenArrayThenGeneratesExtensionWithNullableType(string scalar)
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
                    params int[] values)
                {
                    subject.ThrowIfNull("subject");
            
                    int[]? value = values;

                    if (subject.TestProperty != null)
                    {
                        value = new int[value.Length + subject.TestProperty.Length];

                        subject.TestProperty.CopyTo(value, 0);
                        values.CopyTo(value, subject.TestProperty.Length);
                    }

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
                Member = new()
                {
                    Name = "int",
                },
                Pattern = Pattern.Array,
                Type = new()
                {
                    IsNullable = true,
                    Name = "int[]",
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
        _ = result.Should().Be(expected);
    }

    [Theory]
    [InlineData("throw new NotImplementedException();")]
    [InlineData("return new();")]
    public void GivenBuildablePropertyWhenArrayThenGeneratesDelegateExtension(string scalar)
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
                    params TestType[] values)
                {
                    subject.ThrowIfNull("subject");
            
                    TestType[] value = values;
            
                    if (subject.TestProperty != null)
                    {
                        value = new TestType[value.Length + subject.TestProperty.Length];
            
                        subject.TestProperty.CopyTo(value, 0);
                        values.CopyTo(value, subject.TestProperty.Length);
                    }
            
                    {{scalar}}
                }

                public static global::TestSubject WithTestProperty(
                    this global::TestSubject subject,
                    Func<TestType, TestType> builder)
                {
                    subject.ThrowIfNull("subject");
            
                    builder.ThrowIfNull("builder");
            
                    var instance = new TestType();
            
                    instance = builder(instance);
            
                    return subject.WithTestProperty(instance);
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
                Member = new()
                {
                    IsBuildable = true,
                    Name = "TestType",
                },
                Pattern = Pattern.Array,
                Type = new()
                {
                    Name = "TestType[]",
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
        _ = result.Should().Be(expected);
    }
}