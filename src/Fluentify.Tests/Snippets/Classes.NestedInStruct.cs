namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string NestedInStructContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            public struct Outter
            {
                [Fluentify]
                public sealed class NestedInStruct
                {
                    public int Age { get; set; }

                    public string Name { get; set; }

                    public IReadOnlyList<object> Attributes { get; set; }
                }
            }
        }
        """;

    public static readonly Declared NestedInStruct;

    public static readonly Generated NestedInStructWithAgeExtensions = new(
        NestedInStructWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInStructExtensions.WithAge");

    public static readonly Generated NestedInStructWithAttributesExtensions = new(
        NestedInStructWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInStructExtensions.WithAttributes");

    public static readonly Generated NestedInStructWithNameExtensions = new(
        NestedInStructWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInStructExtensions.WithName");

    public static readonly Generated NestedInStructWithExtensions = new(
        NestedInStructWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInStructExtensions.With");

    private const string NestedInStructWithAgeExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class OutterNestedInStructExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter.NestedInStruct WithAge(this global::Fluentify.Classes.Testing.Outter.NestedInStruct subject, int value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    return new global::Fluentify.Classes.Testing.Outter.NestedInStruct
                    {
                        Age = value,
                        Attributes = subject.Attributes,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string NestedInStructWithAttributesExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class OutterNestedInStructExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter.NestedInStruct WithAttributes(this global::Fluentify.Classes.Testing.Outter.NestedInStruct subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Classes.Testing.Outter.NestedInStruct WithAttributes(this global::Fluentify.Classes.Testing.Outter.NestedInStruct subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.Outter.NestedInStruct WithAttributes(this global::Fluentify.Classes.Testing.Outter.NestedInStruct subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.Outter.NestedInStruct WithAttributes(this global::Fluentify.Classes.Testing.Outter.NestedInStruct subject, params object[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    global::System.Collections.Generic.IReadOnlyList<object> value = values;

                    if (subject.Attributes != null)
                    {
                        value = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Union(subject.Attributes, values));
                    }

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.Outter.NestedInStruct
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string NestedInStructWithNameExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class OutterNestedInStructExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter.NestedInStruct WithName(this global::Fluentify.Classes.Testing.Outter.NestedInStruct subject, string value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.Outter.NestedInStruct
                    {
                        Age = subject.Age,
                        Attributes = subject.Attributes,
                        Name = value,
                    };
                }
            }
        }
        """;

    private const string NestedInStructWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class OutterNestedInStructExtensions
            {
                internal static global::Fluentify.Classes.Testing.Outter.NestedInStruct With(this global::Fluentify.Classes.Testing.Outter.NestedInStruct subject, global::System.Func<int> age = default, global::System.Func<global::System.Collections.Generic.IReadOnlyList<object>> attributes = default, global::System.Func<string> name = default)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    var ageValue = global::System.Object.ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = global::System.Object.ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = global::System.Object.ReferenceEquals(name, null) ? subject.Name : name();

                    global::Fluentify.Internal.Extensions.ThrowIfNull(attributesValue, "attributes");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(nameValue, "name");

                    return new global::Fluentify.Classes.Testing.Outter.NestedInStruct
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