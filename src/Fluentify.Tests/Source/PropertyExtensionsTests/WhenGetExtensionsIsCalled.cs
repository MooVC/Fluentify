namespace Fluentify.Source.PropertyExtensionsTests;

using Fluentify.Model;
using Microsoft.CodeAnalysis;
using Metadata = Fluentify.Source.Metadata;

public sealed class WhenGetExtensionsIsCalled
{
    [Theory]
    [InlineData("throw new NotImplementedException();")]
    [InlineData("return new();")]
    public void GivenPublicPropertyAndPublicSubjectThenGeneratesPublicExtension(string scalar)
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
            Name = "TestSubject",
            Accessibility = Accessibility.Public,
            Properties = [],
        };

        var property = new Property
        {
            Name = "TestProperty",
            Accessibility = Accessibility.Public,
            Descriptor = "WithTestProperty",
            Type = "int",
            IsNullable = false,
            IsBuildable = false,
        };

        var metadata = new Metadata
        {
            Subject = subject,
            Constraints = [],
            Parameters = string.Empty,
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
            Name = "TestSubject",
            Accessibility = Accessibility.Public,
            Properties = [],
        };

        var property = new Property
        {
            Name = "TestProperty",
            Accessibility = Accessibility.Internal,
            Descriptor = "WithTestProperty",
            Type = "int",
            IsNullable = false,
            IsBuildable = false,
        };

        var metadata = new Metadata
        {
            Subject = subject,
            Constraints = [],
            Parameters = string.Empty,
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
            Name = "TestSubject",
            Accessibility = Accessibility.Public,
            Properties = [],
        };

        var property = new Property
        {
            Name = "TestProperty",
            Accessibility = Accessibility.Public,
            Descriptor = "WithTestProperty",
            Type = "int",
            IsNullable = true,
            IsBuildable = false,
        };

        var metadata = new Metadata
        {
            Subject = subject,
            Constraints = [],
            Parameters = string.Empty,
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

            public static partial class TestSubjectExtensions
            {
                public static TestSubject WithTestProperty(this TestSubject subject, TestType value)
                {
                    ArgumentNullException.ThrowIfNull(subject);

                    {{scalar}}
                }

                public static TestSubject WithTestProperty(this TestSubject subject, global::Fluentify.Builder<TestType> value)
                {
                    ArgumentNullException.ThrowIfNull(subject);

                    var instance = new TestType();

                    return subject.WithTestProperty(instance);
                }
            }
            """;

        var subject = new Subject
        {
            Name = "TestSubject",
            Accessibility = Accessibility.Public,
            Properties = [],
        };

        var property = new Property
        {
            Name = "TestProperty",
            Accessibility = Accessibility.Public,
            Descriptor = "WithTestProperty",
            Type = "TestType",
            IsNullable = false,
            IsBuildable = true,
        };

        var metadata = new Metadata
        {
            Subject = subject,
            Constraints = [],
            Parameters = string.Empty,
            Type = "TestSubject",
        };

        // Act
        string result = property.GetExtensions(ref metadata, _ => scalar);

        // Assert
        _ = result.Should().Be(expected);
    }
}