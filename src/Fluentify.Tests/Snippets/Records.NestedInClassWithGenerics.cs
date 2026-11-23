namespace Fluentify.Snippets;

public static partial class Records
{
    public const string NestedInClassWithGenericsContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            public sealed partial class Outter<TOutter>
                where TOutter : class
            {
                [Fluentify]
                public sealed partial record NestedInClassWithGenerics<TInner>(int Age, string Name, IReadOnlyList<object>? Attributes = default)
                    where TInner : struct;
            }
        }
        """;

    public static readonly Declared NestedInClassWithGenerics;

    public static readonly Generated NestedInClassWithGenericsConstructor = new(
        NestedInClassWithGenericsConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInClassWithGenerics.ctor");

    public static readonly Generated NestedInClassWithGenericsWithAgeExtensions = new(
        NestedInClassWithGenericsWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInClassWithGenericsExtensions.WithAge");

    public static readonly Generated NestedInClassWithGenericsWithAttributesExtensions = new(
        NestedInClassWithGenericsWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInClassWithGenericsExtensions.WithAttributes");

    public static readonly Generated NestedInClassWithGenericsWithNameExtensions = new(
        NestedInClassWithGenericsWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInClassWithGenericsExtensions.WithName");

    private const string NestedInClassWithGenericsConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            partial class Outter<TOutter>
            {
                partial record NestedInClassWithGenerics<TInner>
                {
                    #pragma warning disable CS8604

                    #if NET7_0_OR_GREATER
                    [global::System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
                    #endif
                    public NestedInClassWithGenerics()
                        : this(default(int), default(string), default(global::System.Collections.Generic.IReadOnlyList<object>?))
                    {
                    }

                    #pragma warning restore CS8604
                }
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string NestedInClassWithGenericsWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class OutterNestedInClassWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> WithAge<TOutter, TInner>(this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> subject, int value)
                    where TOutter : class
                    where TInner : struct
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    return subject with
                    {
                        Age = value,
                    };
                }
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string NestedInClassWithGenericsWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class OutterNestedInClassWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> WithAttributes<TOutter, TInner>(this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> WithAttributes<TOutter, TInner>(this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> subject, object instance, global::System.Func<object, object> builder)
                    where TOutter : class
                    where TInner : struct
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> WithAttributes<TOutter, TInner>(this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> subject, global::System.Func<object, object> builder)
                    where TOutter : class
                    where TInner : struct
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> WithAttributes<TOutter, TInner>(this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> subject, params object[] values)
                    where TOutter : class
                    where TInner : struct
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    global::System.Collections.Generic.IReadOnlyList<object>? value = values;

                    if (subject.Attributes != null)
                    {
                        value = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Union(subject.Attributes, values));
                    }

                    return subject with
                    {
                        Attributes = value,
                    };
                }
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string NestedInClassWithGenericsWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class OutterNestedInClassWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> WithName<TOutter, TInner>(this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> subject, string value)
                    where TOutter : class
                    where TInner : struct
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return subject with
                    {
                        Name = value,
                    };
                }
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;
}