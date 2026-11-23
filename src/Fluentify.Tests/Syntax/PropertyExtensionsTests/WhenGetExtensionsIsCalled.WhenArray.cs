namespace Fluentify.Syntax.PropertyExtensionsTests;

using Fluentify.Model;
using Microsoft.CodeAnalysis;

public sealed partial class WhenGetExtensionsIsCalled
{
    [Test]
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
                    IsFrameworkType = true,
                    IsValueType = true,
                    Name = "int",
                },
                Pattern = Pattern.Array,
                Type = new()
                {
                    IsFrameworkType = true,
                    Name = "int[]",
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
    public void GivenBuildablePropertyWhenArrayThenGeneratesDelegateExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            public static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(this global::TestSubject subject, params TestType[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    TestType[] value = values;

                    if (subject.TestProperty != null)
                    {
                        value = new TestType[value.Length + subject.TestProperty.Length];

                        subject.TestProperty.CopyTo(value, 0);
                        values.CopyTo(value, subject.TestProperty.Length);
                    }

                    {{scalar}}
                }

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
                Pattern = Pattern.Array,
                Type = new()
                {
                    Initialization = "new TestType()",
                    Name = "TestType[]",
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
    public void GivenInternalPropertyAndPublicSubjectWhenArrayThenGeneratesInternalExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            public static partial class TestSubjectExtensions
            {
                internal static global::TestSubject WithTestProperty(this global::TestSubject subject, params int[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

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
                    IsFrameworkType = true,
                    IsValueType = true,
                    Name = "int",
                },
                Pattern = Pattern.Array,
                Type = new()
                {
                    IsFrameworkType = true,
                    Name = "int[]",
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
    public void GivenNullablePropertyWhenArrayThenGeneratesExtensionWithNullableType(string scalar)
    {
        string expected = $$"""
            public static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(this global::TestSubject subject, params int[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

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
                    IsFrameworkType = true,
                    IsValueType = true,
                    Name = "int",
                },
                Pattern = Pattern.Array,
                Type = new()
                {
                    IsFrameworkType = true,
                    IsNullable = true,
                    Name = "int[]",
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
    public void GivenPublicPropertyAndPublicSubjectWhenArrayThenGeneratesPublicExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            public static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(this global::TestSubject subject, params int[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

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
                    IsFrameworkType = true,
                    IsValueType = true,
                    Name = "int",
                },
                Pattern = Pattern.Array,
                Type = new()
                {
                    IsFrameworkType = true,
                    Name = "int[]",
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