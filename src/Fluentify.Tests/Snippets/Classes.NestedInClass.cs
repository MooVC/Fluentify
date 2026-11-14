namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string NestedInClassContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            public sealed class Outter
            {
                [Fluentify]
                public sealed class NestedInClass
                {
                    public int Age { get; set; }

                    public string Name { get; set; }

                    public IReadOnlyList<object> Attributes { get; set; }
                }
            }
        }
        """;

    public static readonly Declared NestedInClass;

    public static readonly Generated NestedInClassWithAgeExtensions = new(
        NestedInClassWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInClassExtensions.WithAge");

    public static readonly Generated NestedInClassWithAttributesExtensions = new(
        NestedInClassWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInClassExtensions.WithAttributes");

    public static readonly Generated NestedInClassWithNameExtensions = new(
        NestedInClassWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInClassExtensions.WithName");

    public static readonly Generated NestedInClassWithExtensions = new(
        NestedInClassWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInClassExtensions.With");

    private const string NestedInClassWithAgeExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class OutterNestedInClassExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter.NestedInClass WithAge(this global::Fluentify.Classes.Testing.Outter.NestedInClass subject, int value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    return new global::Fluentify.Classes.Testing.Outter.NestedInClass
                    {
                        Age = value,
                        Attributes = subject.Attributes,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string NestedInClassWithAttributesExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class OutterNestedInClassExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter.NestedInClass WithAttributes(this global::Fluentify.Classes.Testing.Outter.NestedInClass subject, global::System.Func<object, object> builder, params object[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    foreach (var value in values)
                    {
                        subject = subject.WithAttributes(value, builder);
                    }

                    return subject;
                }

                public static global::Fluentify.Classes.Testing.Outter.NestedInClass WithAttributes(this global::Fluentify.Classes.Testing.Outter.NestedInClass subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.Outter.NestedInClass WithAttributes(this global::Fluentify.Classes.Testing.Outter.NestedInClass subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.Outter.NestedInClass WithAttributes(this global::Fluentify.Classes.Testing.Outter.NestedInClass subject, params object[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    global::System.Collections.Generic.IReadOnlyList<object> value = values;

                    if (subject.Attributes != null)
                    {
                        value = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Union(subject.Attributes, values));
                    }

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.Outter.NestedInClass
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string NestedInClassWithNameExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class OutterNestedInClassExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter.NestedInClass WithName(this global::Fluentify.Classes.Testing.Outter.NestedInClass subject, string value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.Outter.NestedInClass
                    {
                        Age = subject.Age,
                        Attributes = subject.Attributes,
                        Name = value,
                    };
                }
            }
        }
        """;

    private const string NestedInClassWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class OutterNestedInClassExtensions
            {
                internal static global::Fluentify.Classes.Testing.Outter.NestedInClass With(this global::Fluentify.Classes.Testing.Outter.NestedInClass subject, global::System.Func<int> age = default, global::System.Func<global::System.Collections.Generic.IReadOnlyList<object>> attributes = default, global::System.Func<string> name = default)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    var ageValue = global::System.Object.ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = global::System.Object.ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = global::System.Object.ReferenceEquals(name, null) ? subject.Name : name();

                    global::Fluentify.Internal.Extensions.ThrowIfNull(attributesValue, "attributes");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(nameValue, "name");

                    return new global::Fluentify.Classes.Testing.Outter.NestedInClass
                    {
                        Age = ageValue,
                        Attributes = attributesValue,
                        Name = nameValue,
                    };
                }
            }
        }
        """;
}