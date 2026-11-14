namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string MultipleGenericsContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class MultipleGenerics<T1, T2, T3>
                where T1 : struct
                where T2 : class, new()
                where T3 : IEnumerable<string>
            {
                public T1 Age { get; set; }

                public T2 Name { get; set; }

                public T3 Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared MultipleGenerics;

    public static readonly Generated MultipleGenericsWithAgeExtensions = new(
        MultipleGenericsWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.MultipleGenericsExtensions.WithAge");

    public static readonly Generated MultipleGenericsWithAttributesExtensions = new(
        MultipleGenericsWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.MultipleGenericsExtensions.WithAttributes");

    public static readonly Generated MultipleGenericsWithNameExtensions = new(
        MultipleGenericsWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.MultipleGenericsExtensions.WithName");

    public static readonly Generated MultipleGenericsWithExtensions = new(
        MultipleGenericsWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.MultipleGenericsExtensions.With");

    private const string MultipleGenericsWithAgeExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class MultipleGenericsExtensions
            {
                public static global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> WithAge<T1, T2, T3>(this global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> subject, T1 instance, global::System.Func<T1, T1> builder)
                    where T1 : struct
                    where T2 : class, new()
                    where T3 : global::System.Collections.Generic.IEnumerable<string>
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAge(instance);
                }

                public static global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> WithAge<T1, T2, T3>(this global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> subject, global::System.Func<T1, T1> builder)
                    where T1 : struct
                    where T2 : class, new()
                    where T3 : global::System.Collections.Generic.IEnumerable<string>
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = subject.Age;

                    instance = builder(instance);

                    return subject.WithAge(instance);
                }

                public static global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> WithAge<T1, T2, T3>(this global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> subject, T1 value)
                    where T1 : struct
                    where T2 : class, new()
                    where T3 : global::System.Collections.Generic.IEnumerable<string>
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    return new global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3>
                    {
                        Age = value,
                        Attributes = subject.Attributes,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string MultipleGenericsWithAttributesExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class MultipleGenericsExtensions
            {
                public static global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> WithAttributes<T1, T2, T3>(this global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> subject, T3 instance, global::System.Func<T3, T3> builder)
                    where T1 : struct
                    where T2 : class, new()
                    where T3 : global::System.Collections.Generic.IEnumerable<string>
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> WithAttributes<T1, T2, T3>(this global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> subject, global::System.Func<T3, T3> builder)
                    where T1 : struct
                    where T2 : class, new()
                    where T3 : global::System.Collections.Generic.IEnumerable<string>
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = subject.Attributes;

                    if (global::System.Object.ReferenceEquals(instance, null))
                    {
                        throw new global::System.NotSupportedException();
                    }

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> WithAttributes<T1, T2, T3>(this global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> subject, T3 value)
                    where T1 : struct
                    where T2 : class, new()
                    where T3 : global::System.Collections.Generic.IEnumerable<string>
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3>
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string MultipleGenericsWithNameExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class MultipleGenericsExtensions
            {
                public static global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> WithName<T1, T2, T3>(this global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> subject, T2 instance, global::System.Func<T2, T2> builder)
                    where T1 : struct
                    where T2 : class, new()
                    where T3 : global::System.Collections.Generic.IEnumerable<string>
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithName(instance);
                }

                public static global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> WithName<T1, T2, T3>(this global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> subject, global::System.Func<T2, T2> builder)
                    where T1 : struct
                    where T2 : class, new()
                    where T3 : global::System.Collections.Generic.IEnumerable<string>
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = subject.Name;

                    if (global::System.Object.ReferenceEquals(instance, null))
                    {
                        instance = new T2();
                    }

                    instance = builder(instance);

                    return subject.WithName(instance);
                }

                public static global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> WithName<T1, T2, T3>(this global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> subject, T2 value)
                    where T1 : struct
                    where T2 : class, new()
                    where T3 : global::System.Collections.Generic.IEnumerable<string>
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3>
                    {
                        Age = subject.Age,
                        Attributes = subject.Attributes,
                        Name = value,
                    };
                }
            }
        }
        """;

    private const string MultipleGenericsWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class MultipleGenericsExtensions
            {
                internal static global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> With<T1, T2, T3>(this global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> subject, global::System.Func<T1> age = default, global::System.Func<T3> attributes = default, global::System.Func<T2> name = default)
                    where T1 : struct
                    where T2 : class, new()
                    where T3 : global::System.Collections.Generic.IEnumerable<string>
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    var ageValue = global::System.Object.ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = global::System.Object.ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = global::System.Object.ReferenceEquals(name, null) ? subject.Name : name();

                    global::Fluentify.Internal.Extensions.ThrowIfNull(attributesValue, "attributes");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(nameValue, "name");

                    return new global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3>
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