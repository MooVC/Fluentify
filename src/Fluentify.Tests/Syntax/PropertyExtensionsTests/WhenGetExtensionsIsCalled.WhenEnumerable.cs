namespace Fluentify.Syntax.PropertyExtensionsTests;

using Fluentify.Model;
using Microsoft.CodeAnalysis;

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

        // Act
        string result = property.GetExtensions(subject, _ => default).ToString();

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
            public static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(this global::TestSubject subject, global::System.Func<TestType, TestType> builder, params TestType[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    foreach (var value in values)
                    {
                        subject = subject.WithTestProperty(value, builder);
                    }

                    return subject;
                }

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

                    var instance = new TestType();

                    instance = builder(instance);

                    return subject.WithTestProperty(instance);
                }

                public static global::TestSubject WithTestProperty(this global::TestSubject subject, params TestType[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    {{type}} value = values;

                    if (subject.TestProperty != null)
                    {
                        value = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Union(subject.TestProperty, values));
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

        // Act
        string result = property.GetExtensions(subject, _ => scalar).ToString();

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
            public static partial class TestSubjectExtensions
            {
                internal static global::TestSubject WithTestProperty(this global::TestSubject subject, params int[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    {{type}} value = values;

                    if (subject.TestProperty != null)
                    {
                        value = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Union(subject.TestProperty, values));
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

        // Act
        string result = property.GetExtensions(subject, _ => scalar).ToString();

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
            public static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(this global::TestSubject subject, params int[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    {{type}}? value = values;

                    if (subject.TestProperty != null)
                    {
                        value = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Union(subject.TestProperty, values));
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

        // Act
        string result = property.GetExtensions(subject, _ => scalar).ToString();

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
            public static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(this global::TestSubject subject, params int[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    {{type}} value = values;

                    if (subject.TestProperty != null)
                    {
                        value = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Union(subject.TestProperty, values));
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

        // Act
        string result = property.GetExtensions(subject, _ => scalar).ToString();

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
            public static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(this global::TestSubject subject, params int[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

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

        // Act
        string result = property.GetExtensions(subject, _ => scalar).ToString();

        // Assert
        result.ShouldBe(expected);
    }
}