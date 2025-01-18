namespace Fluentify.Source.PropertyExtensionsTests;

using Fluentify.Model;
using Microsoft.CodeAnalysis;
using Metadata = Fluentify.Source.Metadata;

public sealed partial class WhenGetExtensionsIsCalled
{
    [Theory]
    [InlineData("IEnumerable<int>")]
    [InlineData("IReadOnlyCollection<int>")]
    [InlineData("IReadOnlyList<int>")]
    public void GivenAScalarThenYieldsNullWhenEnumerableThenNoExtensionIsGenerated(string type)
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
                Pattern = Pattern.Enumerable,
                Type = new()
                {
                    Name = type,
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
    [InlineData("throw new NotImplementedException();", "IEnumerable<int>")]
    [InlineData("return new();", "IEnumerable<int>")]
    [InlineData("throw new NotImplementedException();", "IReadOnlyCollection<int>")]
    [InlineData("return new();", "IReadOnlyCollection<int>")]
    [InlineData("throw new NotImplementedException();", "IReadOnlyList<int>")]
    [InlineData("return new();", "IReadOnlyList<int>")]
    public void GivenPublicPropertyAndPublicSubjectWhenEnumerableThenGeneratesPublicExtension(string scalar, string type)
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

                    {{type}} value = values;
            
                    if (subject.TestProperty != null)
                    {
                        value = subject.TestProperty
                            .Union(values)
                            .ToArray();
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
                Pattern = Pattern.Enumerable,
                Type = new()
                {
                    Name = type,
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
    [InlineData("throw new NotImplementedException();", "IEnumerable<int>")]
    [InlineData("return new();", "IEnumerable<int>")]
    [InlineData("throw new NotImplementedException();", "IReadOnlyCollection<int>")]
    [InlineData("return new();", "IReadOnlyCollection<int>")]
    [InlineData("throw new NotImplementedException();", "IReadOnlyList<int>")]
    [InlineData("return new();", "IReadOnlyList<int>")]
    public void GivenInternalPropertyAndPublicSubjectWhenEnumerableThenGeneratesInternalExtension(string scalar, string type)
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

                    {{type}} value = values;

                    if (subject.TestProperty != null)
                    {
                        value = subject.TestProperty
                            .Union(values)
                            .ToArray();
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
                Pattern = Pattern.Enumerable,
                Type = new()
                {
                    Name = type,
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
    [InlineData("throw new NotImplementedException();", "IEnumerable<int>")]
    [InlineData("return new();", "IEnumerable<int>")]
    [InlineData("throw new NotImplementedException();", "IReadOnlyCollection<int>")]
    [InlineData("return new();", "IReadOnlyCollection<int>")]
    [InlineData("throw new NotImplementedException();", "IReadOnlyList<int>")]
    [InlineData("return new();", "IReadOnlyList<int>")]
    public void GivenNullablePropertyWhenEnumerableThenGeneratesExtensionWithNullableType(string scalar, string type)
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

                    {{type}}? value = values;

                    if (subject.TestProperty != null)
                    {
                        value = subject.TestProperty
                            .Union(values)
                            .ToArray();
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
                Pattern = Pattern.Enumerable,
                Type = new()
                {
                    IsNullable = true,
                    Name = type,
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
    [InlineData("throw new NotImplementedException();", "IEnumerable<TestType>")]
    [InlineData("return new();", "IEnumerable<TestType>")]
    [InlineData("throw new NotImplementedException();", "IReadOnlyCollection<TestType>")]
    [InlineData("return new();", "IReadOnlyCollection<TestType>")]
    [InlineData("throw new NotImplementedException();", "IReadOnlyList<TestType>")]
    [InlineData("return new();", "IReadOnlyList<TestType>")]
    public void GivenBuildablePropertyWhenEnumerableThenGeneratesDelegateExtension(string scalar, string type)
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

                    {{type}} value = values;
            
                    if (subject.TestProperty != null)
                    {
                        value = subject.TestProperty
                            .Union(values)
                            .ToArray();
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
                Pattern = Pattern.Enumerable,
                Type = new()
                {
                    Name = type,
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
}