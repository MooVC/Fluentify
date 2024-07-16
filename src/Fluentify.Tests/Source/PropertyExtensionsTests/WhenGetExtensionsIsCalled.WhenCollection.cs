namespace Fluentify.Source.PropertyExtensionsTests;

using Fluentify.Model;
using Microsoft.CodeAnalysis;
using Metadata = Fluentify.Source.Metadata;

public sealed partial class WhenGetExtensionsIsCalled
{
    [Fact]
    public void GivenAScalarThenYieldsNullWhenCollectionThenNoExtensionIsGenerated()
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
                Member = new()
                {
                    Name = "int",
                },
                Pattern = Pattern.Collection,
                Type = new()
                {
                    Name = "List<int>",
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
    public void GivenPublicPropertyAndPublicSubjectWhenCollectionThenGeneratesPublicExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            using System;
            using System.Collections.Generic;
            using System.Linq;

            public static partial class TestSubjectExtensions
            {
                public static TestSubject WithTestProperty(this TestSubject subject, params int[] values)
                {
                    ArgumentNullException.ThrowIfNull(subject);

                    List<int> value = new();
            
                    if (subject.TestProperty is not null)
                    {
                        foreach (var element in subject.TestProperty)
                        {
                            value.Add(element);
                        }
                    }
            
                    foreach (var element in values)
                    {
                        value.Add(element);
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
                Pattern = Pattern.Collection,
                Type = new()
                {
                    Name = "List<int>",
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
    public void GivenInternalPropertyAndPublicSubjectWhenCollectionThenGeneratesInternalExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            using System;
            using System.Collections.Generic;
            using System.Linq;

            internal static partial class TestSubjectExtensions
            {
                public static TestSubject WithTestProperty(this TestSubject subject, params int[] values)
                {
                    ArgumentNullException.ThrowIfNull(subject);

                    List<int> value = new();
            
                    if (subject.TestProperty is not null)
                    {
                        foreach (var element in subject.TestProperty)
                        {
                            value.Add(element);
                        }
                    }
            
                    foreach (var element in values)
                    {
                        value.Add(element);
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
                Pattern = Pattern.Collection,
                Type = new()
                {
                    Name = "List<int>",
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
    public void GivenNullablePropertyWhenCollectionThenGeneratesExtensionWithNullableType(string scalar)
    {
        string expected = $$"""
            using System;
            using System.Collections.Generic;
            using System.Linq;

            public static partial class TestSubjectExtensions
            {
                public static TestSubject WithTestProperty(this TestSubject subject, params int[] values)
                {
                    ArgumentNullException.ThrowIfNull(subject);
            
                    List<int>? value = new();
            
                    if (subject.TestProperty is not null)
                    {
                        foreach (var element in subject.TestProperty)
                        {
                            value.Add(element);
                        }
                    }
            
                    foreach (var element in values)
                    {
                        value.Add(element);
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
                Pattern = Pattern.Collection,
                Type = new()
                {
                    IsNullable = true,
                    Name = "List<int>",
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
    public void GivenBuildablePropertyWhenCollectionThenGeneratesDelegateExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            using System;
            using System.Collections.Generic;
            using System.Linq;

            public static partial class TestSubjectExtensions
            {
                public static TestSubject WithTestProperty(this TestSubject subject, params TestType[] values)
                {
                    ArgumentNullException.ThrowIfNull(subject);
            
                    List<TestType> value = new();
            
                    if (subject.TestProperty is not null)
                    {
                        foreach (var element in subject.TestProperty)
                        {
                            value.Add(element);
                        }
                    }
            
                    foreach (var element in values)
                    {
                        value.Add(element);
                    }

                    {{scalar}}
                }

                public static TestSubject WithTestProperty(this TestSubject subject, Func<TestType, TestType> builder)
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
                Pattern = Pattern.Collection,
                Type = new()
                {
                    Name = "List<TestType>",
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