namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string NestedInStructWithGenericsContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            public struct Outter<TOutter>
                where TOutter : class
            {
                [Fluentify]
                public sealed class NestedInStructWithGenerics<TInner>
                    where TInner : struct
                {
                    public int Age { get; set; }

                    public string Name { get; set; }

                    public IReadOnlyList<object> Attributes { get; set; }
                }
            }
        }
        """;

    public static readonly Declared NestedInStructWithGenerics;

    public static readonly Generated NestedInStructWithGenericsWithAgeExtensions = new(
        NestedInStructWithGenericsWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInStructWithGenericsExtensions.WithAge");

    public static readonly Generated NestedInStructWithGenericsWithAttributesExtensions = new(
        NestedInStructWithGenericsWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInStructWithGenericsExtensions.WithAttributes");

    public static readonly Generated NestedInStructWithGenericsWithNameExtensions = new(
        NestedInStructWithGenericsWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInStructWithGenericsExtensions.WithName");

    public static readonly Generated NestedInStructWithGenericsWithExtensions = new(
        NestedInStructWithGenericsWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInStructWithGenericsExtensions.With");

    private const string NestedInStructWithGenericsWithAgeExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class OutterNestedInStructWithGenericsExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> WithAge<TOutter, TInner>(this global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject, int value)
                    where TOutter : class
                    where TInner : struct
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    return new global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner>
                    {
                        Age = value,
                        Attributes = subject.Attributes,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string NestedInStructWithGenericsWithAttributesExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class OutterNestedInStructWithGenericsExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> WithAttributes<TOutter, TInner>(this global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject, global::System.Func<object, object> builder, params object[] values)
                    where TOutter : class
                    where TInner : struct
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

                public static global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> WithAttributes<TOutter, TInner>(this global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject, object instance, global::System.Func<object, object> builder)
                    where TOutter : class
                    where TInner : struct
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> WithAttributes<TOutter, TInner>(this global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject, global::System.Func<object, object> builder)
                    where TOutter : class
                    where TInner : struct
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> WithAttributes<TOutter, TInner>(this global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject, params object[] values)
                    where TOutter : class
                    where TInner : struct
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    global::System.Collections.Generic.IReadOnlyList<object> value = values;

                    if (subject.Attributes != null)
                    {
                        value = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Union(subject.Attributes, values));
                    }

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner>
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string NestedInStructWithGenericsWithNameExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class OutterNestedInStructWithGenericsExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> WithName<TOutter, TInner>(this global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject, string value)
                    where TOutter : class
                    where TInner : struct
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner>
                    {
                        Age = subject.Age,
                        Attributes = subject.Attributes,
                        Name = value,
                    };
                }
            }
        }
        """;

    private const string NestedInStructWithGenericsWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class OutterNestedInStructWithGenericsExtensions
            {
                internal static global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> With<TOutter, TInner>(this global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject, global::System.Func<int> age = default, global::System.Func<global::System.Collections.Generic.IReadOnlyList<object>> attributes = default, global::System.Func<string> name = default)
                    where TOutter : class
                    where TInner : struct
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    var ageValue = global::System.Object.ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = global::System.Object.ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = global::System.Object.ReferenceEquals(name, null) ? subject.Name : name();

                    global::Fluentify.Internal.Extensions.ThrowIfNull(attributesValue, "attributes");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(nameValue, "name");

                    return new global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner>
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