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
        string result = property.GetExtensions(ref metadata, _ => default);

        // Assert
        _ = result.Should().BeEmpty();
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
    public void GivenInternalPropertyAndPublicSubjectThenGeneratesInternalExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            using System;
            using System.Collections.Generic;
            using System.Linq;

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
    public void GivenNullablePropertyThenGeneratesExtensionWithNullableType(string scalar)
    {
        string expected = $$"""
            using System;
            using System.Collections.Generic;
            using System.Linq;

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
    public void GivenBuildablePropertyThenGeneratesDelegateExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            using System;
            using System.Collections.Generic;
            using System.Linq;

            public static partial class TestSubjectExtensions
            {
                public static TestSubject WithTestProperty(this TestSubject subject, Func<TestType, TestType> builder)
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