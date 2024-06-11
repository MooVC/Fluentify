namespace Fluentify.Source.PropertyExtensionsTests;

using Fluentify.Model;
using Microsoft.CodeAnalysis;
using Metadata = Fluentify.Source.Metadata;

public sealed class WhenGetExtensionsIsCalled
{
    [Theory]
    [InlineData("throw new NotImplementedException();")]
    [InlineData("return new();")]
    public void GivenPublicPropertyAndPublicSubjectWhenArrayThenGeneratesPublicExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            using System;

            public static partial class TestSubjectExtensions
            {
                public static TestSubject WithTestProperty(this TestSubject subject, params int[] values)
                {
                    ArgumentNullException.ThrowIfNull(subject);

                    int[] value = values;

                    if (subject.TestProperty is not null)
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
            Type = "TestSubject",
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

            internal static partial class TestSubjectExtensions
            {
                public static TestSubject WithTestProperty(this TestSubject subject, params int[] values)
                {
                    ArgumentNullException.ThrowIfNull(subject);

                    int[] value = values;

                    if (subject.TestProperty is not null)
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
            Type = "TestSubject",
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

            public static partial class TestSubjectExtensions
            {
                public static TestSubject WithTestProperty(this TestSubject subject, params int[] values)
                {
                    ArgumentNullException.ThrowIfNull(subject);
            
                    int[]? value = values;

                    if (subject.TestProperty is not null)
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
            Type = "TestSubject",
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

            public static partial class TestSubjectExtensions
            {
                public static TestSubject WithTestProperty(this TestSubject subject, params TestType[] values)
                {
                    ArgumentNullException.ThrowIfNull(subject);
            
                    TestType[] value = values;
            
                    if (subject.TestProperty is not null)
                    {
                        value = new TestType[value.Length + subject.TestProperty.Length];
            
                        subject.TestProperty.CopyTo(value, 0);
                        values.CopyTo(value, subject.TestProperty.Length);
                    }
            
                    {{scalar}}
                }

                public static TestSubject WithTestProperty(this TestSubject subject, global::Fluentify.Builder<TestType> builder)
                {
                    ArgumentNullException.ThrowIfNull(subject);
            
                    ArgumentNullException.ThrowIfNull(builder);
            
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
            Type = "TestSubject",
        };

        // Act
        string result = property.GetExtensions(ref metadata, _ => scalar);

        // Assert
        _ = result.Should().Be(expected);
    }

    [Theory]
    [InlineData("throw new NotImplementedException();")]
    [InlineData("return new();")]
    public void GivenPublicPropertyAndPublicSubjectWhenScalarThenGeneratesPublicExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            using System;

            public static partial class TestSubjectExtensions
            {
                public static TestSubject WithTestProperty(this TestSubject subject, int value)
                {
                    ArgumentNullException.ThrowIfNull(subject);

                    {{scalar}}
                }
            }
            """;

        var subject = new Subject
        {
            Accessibility = Accessibility.Public,
            Name = "TestSubject",
            Properties = [],
        };

        var property = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "WithTestProperty",
            Kind = new()
            {
                Type = new()
                {
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
            Type = "TestSubject",
        };

        // Act
        string result = property.GetExtensions(ref metadata, _ => scalar);

        // Assert
        _ = result.Should().Be(expected);
    }

    [Theory]
    [InlineData("throw new NotImplementedException();")]
    [InlineData("return new();")]
    public void GivenInternalPropertyAndPublicSubjectWhenScalarThenGeneratesInternalExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            using System;

            internal static partial class TestSubjectExtensions
            {
                public static TestSubject WithTestProperty(this TestSubject subject, int value)
                {
                    ArgumentNullException.ThrowIfNull(subject);

                    {{scalar}}
                }
            }
            """;

        var subject = new Subject
        {
            Accessibility = Accessibility.Public,
            Name = "TestSubject",
            Properties = [],
        };

        var property = new Property
        {
            Accessibility = Accessibility.Internal,
            Descriptor = "WithTestProperty",
            Kind = new()
            {
                Type = new()
                {
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
            Type = "TestSubject",
        };

        // Act
        string result = property.GetExtensions(ref metadata, _ => scalar);

        // Assert
        _ = result.Should().Be(expected);
    }

    [Theory]
    [InlineData("throw new NotImplementedException();")]
    [InlineData("return new();")]
    public void GivenNullablePropertyWhenScalarThenGeneratesExtensionWithNullableType(string scalar)
    {
        string expected = $$"""
            using System;

            public static partial class TestSubjectExtensions
            {
                public static TestSubject WithTestProperty(this TestSubject subject, int? value)
                {
                    ArgumentNullException.ThrowIfNull(subject);

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
        };

        var property = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "WithTestProperty",
            Kind = new()
            {
                Type = new()
                {
                    IsNullable = true,
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
            Type = "TestSubject",
        };

        // Act
        string result = property.GetExtensions(ref metadata, _ => scalar);

        // Assert
        _ = result.Should().Be(expected);
    }

    [Theory]
    [InlineData("throw new NotImplementedException();")]
    [InlineData("return new();")]
    public void GivenBuildablePropertyWhenScalarThenGeneratesDelegateExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            using System;

            public static partial class TestSubjectExtensions
            {
                public static TestSubject WithTestProperty(this TestSubject subject, global::Fluentify.Builder<TestType> builder)
                {
                    ArgumentNullException.ThrowIfNull(subject);
            
                    ArgumentNullException.ThrowIfNull(builder);
            
                    var instance = new TestType();
            
                    instance = builder(instance);
            
                    return subject.WithTestProperty(instance);
                }

                public static TestSubject WithTestProperty(this TestSubject subject, TestType value)
                {
                    ArgumentNullException.ThrowIfNull(subject);

                    {{scalar}}
                }
            }
            """;

        var subject = new Subject
        {
            Accessibility = Accessibility.Public,
            Name = "TestSubject",
            Properties = [],
        };

        var property = new Property
        {
            Accessibility = Accessibility.Public,
            Descriptor = "WithTestProperty",
            Kind = new()
            {
                Type = new()
                {
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
            Type = "TestSubject",
        };

        // Act
        string result = property.GetExtensions(ref metadata, _ => scalar);

        // Assert
        _ = result.Should().Be(expected);
    }
}