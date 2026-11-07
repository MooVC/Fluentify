namespace Fluentify.Source.PropertyExtensionsTests;

using Fluentify.Model;
using Microsoft.CodeAnalysis;
using Metadata = Fluentify.Source.Metadata;

public sealed partial class WhenGetExtensionsIsCalled
{
    [Test]
    [Arguments("IEnumerable<int>")]
    [Arguments("IReadOnlyCollection<int>")]
    [Arguments("IReadOnlyList<int>")]
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
                    IsFrameworkType = true,
                    IsValueType = true,
                    Name = "int",
                },
                Pattern = Pattern.Enumerable,
                Type = new()
                {
                    IsFrameworkType = true,
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

    [Test]
    [Arguments("throw new NotImplementedException();", "IEnumerable<TestType>")]
    [Arguments("return new();", "IEnumerable<TestType>")]
    [Arguments("throw new NotImplementedException();", "IReadOnlyCollection<TestType>")]
    [Arguments("return new();", "IReadOnlyCollection<TestType>")]
    [Arguments("throw new NotImplementedException();", "IReadOnlyList<TestType>")]
    [Arguments("return new();", "IReadOnlyList<TestType>")]
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
                    Func<TestType, TestType> builder,
                    params TestType[] values)
                {
                    subject.ThrowIfNull("subject");
                    builder.ThrowIfNull("builder");
                    values.ThrowIfNull("values");

                    foreach (var value in values)
                    {
                        subject = subject.WithTestProperty(value, builder);
                    }

                    return subject;
                }

                public static global::TestSubject WithTestProperty(
                    this global::TestSubject subject,
                    TestType instance,
                    Func<TestType, TestType> builder)
                {
                    subject.ThrowIfNull("subject");
                    builder.ThrowIfNull("builder");

                    instance = builder(instance);

                    return subject.WithTestProperty(instance);
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

                public static global::TestSubject WithTestProperty(
                    this global::TestSubject subject,
                    params TestType[] values)
                {
                    subject.ThrowIfNull("subject");
                    values.ThrowIfNull("values");

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
                Member = new()
                {
                    Initialization = "new TestType()",
                    IsBuildable = true,
                    Name = "TestType",
                },
                Pattern = Pattern.Enumerable,
                Type = new()
                {
                    Initialization = $"new {type}()",
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

    [Test]
    [Arguments("throw new NotImplementedException();", "IEnumerable<int>")]
    [Arguments("return new();", "IEnumerable<int>")]
    [Arguments("throw new NotImplementedException();", "IReadOnlyCollection<int>")]
    [Arguments("return new();", "IReadOnlyCollection<int>")]
    [Arguments("throw new NotImplementedException();", "IReadOnlyList<int>")]
    [Arguments("return new();", "IReadOnlyList<int>")]
    public void GivenInternalPropertyAndPublicSubjectWhenEnumerableThenGeneratesInternalExtension(string scalar, string type)
    {
        // Arrange
        string expected = $$"""
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class TestSubjectExtensions
            {
                internal static global::TestSubject WithTestProperty(
                    this global::TestSubject subject,
                    params int[] values)
                {
                    subject.ThrowIfNull("subject");
                    values.ThrowIfNull("values");

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
                    IsFrameworkType = true,
                    IsValueType = true,
                    Name = "int",
                },
                Pattern = Pattern.Enumerable,
                Type = new()
                {
                    IsFrameworkType = true,
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

    [Test]
    [Arguments("throw new NotImplementedException();", "IEnumerable<int>")]
    [Arguments("return new();", "IEnumerable<int>")]
    [Arguments("throw new NotImplementedException();", "IReadOnlyCollection<int>")]
    [Arguments("return new();", "IReadOnlyCollection<int>")]
    [Arguments("throw new NotImplementedException();", "IReadOnlyList<int>")]
    [Arguments("return new();", "IReadOnlyList<int>")]
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
                    values.ThrowIfNull("values");

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
                    IsFrameworkType = true,
                    IsValueType = true,
                    Name = "int",
                },
                Pattern = Pattern.Enumerable,
                Type = new()
                {
                    IsFrameworkType = true,
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

    [Test]
    [Arguments("throw new NotImplementedException();", "IEnumerable<int>")]
    [Arguments("return new();", "IEnumerable<int>")]
    [Arguments("throw new NotImplementedException();", "IReadOnlyCollection<int>")]
    [Arguments("return new();", "IReadOnlyCollection<int>")]
    [Arguments("throw new NotImplementedException();", "IReadOnlyList<int>")]
    [Arguments("return new();", "IReadOnlyList<int>")]
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
                    values.ThrowIfNull("values");

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
                    IsFrameworkType = true,
                    IsValueType = true,
                    Name = "int",
                },
                Pattern = Pattern.Enumerable,
                Type = new()
                {
                    IsFrameworkType = true,
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

    [Test]
    [Arguments("throw new NotImplementedException();", "global::System.Collections.Immutable.ImmutableArray<int>")]
    [Arguments("return new();", "global::System.Collections.Immutable.ImmutableArray<int>")]
    [Arguments("throw new NotImplementedException();", "global::System.Collections.Immutable.ImmutableHashSet<int>")]
    [Arguments("return new();", "global::System.Collections.Immutable.ImmutableHashSet<int>")]
    [Arguments("throw new NotImplementedException();", "global::System.Collections.Immutable.ImmutableList<int>")]
    [Arguments("return new();", "global::System.Collections.Immutable.ImmutableList<int>")]
    [Arguments("throw new NotImplementedException();", "global::System.Collections.Immutable.ImmutableSortedSet<int>")]
    [Arguments("return new();", "global::System.Collections.Immutable.ImmutableSortedSet<int>")]
    public void GivenPublicPropertyAndPublicSubjectWhenImmutableEnumerableThenGeneratesPublicExtension(string scalar, string type)
    {
        // Arrange
        string initialization = type switch
        {
            "global::System.Collections.Immutable.ImmutableArray<int>" => $$"""
                    global::System.Collections.Immutable.ImmutableArray<int> value = global::System.Collections.Immutable.ImmutableArray.CreateRange(values);

                    if (!subject.TestProperty.IsDefault)
                    {
                        value = subject.TestProperty.AddRange(values);
                    }
            """,
            "global::System.Collections.Immutable.ImmutableHashSet<int>" => $$"""
                    global::System.Collections.Immutable.ImmutableHashSet<int> value = global::System.Collections.Immutable.ImmutableHashSet.CreateRange(values);

                    if (subject.TestProperty != null)
                    {
                        value = subject.TestProperty.Union(values);
                    }
            """,
            "global::System.Collections.Immutable.ImmutableList<int>" => $$"""
                    global::System.Collections.Immutable.ImmutableList<int> value = global::System.Collections.Immutable.ImmutableList.CreateRange(values);

                    if (subject.TestProperty != null)
                    {
                        value = subject.TestProperty.AddRange(values);
                    }
            """,
            _ => $$"""
                    global::System.Collections.Immutable.ImmutableSortedSet<int> value = global::System.Collections.Immutable.ImmutableSortedSet.CreateRange(values);

                    if (subject.TestProperty != null)
                    {
                        value = subject.TestProperty.Union(values);
                    }
            """,
        };

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
                    values.ThrowIfNull("values");

            {{initialization}}

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
                    IsFrameworkType = true,
                    IsValueType = true,
                    Name = "int",
                },
                Pattern = Pattern.Enumerable,
                Type = new()
                {
                    IsFrameworkType = true,
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