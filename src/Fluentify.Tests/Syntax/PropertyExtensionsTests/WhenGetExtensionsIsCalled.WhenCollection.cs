namespace Fluentify.Syntax.PropertyExtensionsTests;

using Fluentify.Model;
using Microsoft.CodeAnalysis;

public sealed partial class WhenGetExtensionsIsCalled
{
    [Test]
    public void GivenAScalarThenYieldsNullWhenCollectionThenNoExtensionIsGenerated()
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
                Pattern = Pattern.Collection,
                Type = new()
                {
                    IsFrameworkType = true,
                    Name = "List<int>",
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
    public void GivenBuildablePropertyWhenCollectionThenGeneratesDelegateExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            public static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(this global::TestSubject subject, params TestType[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    List<TestType> value = new List<TestType>();

                    if (subject.TestProperty != null)
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
                Pattern = Pattern.Collection,
                Type = new()
                {
                    Initialization = "new List<TestType>()",
                    Name = "List<TestType>",
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
    public void GivenCollectionWithExplicitInitializationThenInitializationIsUsed()
    {
        // Arrange
        const string Scalar = "return subject;";

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
                Pattern = Pattern.Collection,
                Type = new()
                {
                    Initialization = "global::Demo.CustomItems.Create()",
                    Name = "global::Demo.CustomItems",
                },
            },
            Name = "TestProperty",
        };

        // Act
        string result = property.GetExtensions(subject, _ => Scalar).ToString();

        // Assert
        result.ShouldContain("global::Demo.CustomItems value = global::Demo.CustomItems.Create();");
        result.ShouldNotContain("new global::Demo.CustomItems()");
    }

    [Test]
    [Arguments("throw new NotImplementedException();")]
    [Arguments("return new();")]
    public void GivenInternalPropertyAndPublicSubjectWhenCollectionThenGeneratesInternalExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            public static partial class TestSubjectExtensions
            {
                internal static global::TestSubject WithTestProperty(this global::TestSubject subject, params int[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    List<int> value = new List<int>();

                    if (subject.TestProperty != null)
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
                    Initialization = "new int()",
                    IsFrameworkType = true,
                    IsValueType = true,
                    Name = "int",
                },
                Pattern = Pattern.Collection,
                Type = new()
                {
                    Initialization = "new List<int>()",
                    IsFrameworkType = true,
                    Name = "List<int>",
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
    [Arguments("return new int();")]
    public void GivenNullablePropertyWhenCollectionThenGeneratesExtensionWithNullableType(string scalar)
    {
        string expected = $$"""
            public static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(this global::TestSubject subject, params int[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    List<int>? value = new List<int>();

                    if (subject.TestProperty != null)
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
                    Initialization = "new int()",
                    IsFrameworkType = true,
                    IsValueType = true,
                    Name = "int",
                },
                Pattern = Pattern.Collection,
                Type = new()
                {
                    Initialization = "new List<int>()",
                    IsFrameworkType = true,
                    IsNullable = true,
                    Name = "List<int>",
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
    public void GivenPublicPropertyAndPublicSubjectWhenCollectionThenGeneratesPublicExtension(string scalar)
    {
        // Arrange
        string expected = $$"""
            public static partial class TestSubjectExtensions
            {
                public static global::TestSubject WithTestProperty(this global::TestSubject subject, params int[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    List<int> value = new List<int>();

                    if (subject.TestProperty != null)
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
                    Initialization = "new int()",
                    IsFrameworkType = true,
                    IsValueType = true,
                    Name = "int",
                },
                Pattern = Pattern.Collection,
                Type = new()
                {
                    Initialization = "new List<int>()",
                    IsFrameworkType = true,
                    Name = "List<int>",
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