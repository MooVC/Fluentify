namespace Fluentify.Snippets;

public static partial class Records
{
    public const string NestedInStructWithGenericsContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            public partial struct Outter<TOutter>
                where TOutter : class
            {
                [Fluentify]
                public sealed partial record NestedInStructWithGenerics<TInner>(int Age, string Name, IReadOnlyList<object>? Attributes = default)
                    where TInner : struct;
            }
        }
        """;

    public static readonly Declared NestedInStructWithGenerics;

    public static readonly Generated NestedInStructWithGenericsConstructor = new(
        NestedInStructWithGenericsConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInStructWithGenerics.ctor");

    public static readonly Generated NestedInStructWithGenericsWithAgeExtensions = new(
        NestedInStructWithGenericsWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInStructWithGenericsExtensions.WithAge");

    public static readonly Generated NestedInStructWithGenericsWithAttributesExtensions = new(
        NestedInStructWithGenericsWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInStructWithGenericsExtensions.WithAttributes");

    public static readonly Generated NestedInStructWithGenericsWithNameExtensions = new(
        NestedInStructWithGenericsWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInStructWithGenericsExtensions.WithName");

    private const string NestedInStructWithGenericsConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            partial struct Outter<TOutter>
            {
                partial record NestedInStructWithGenerics<TInner>
                {
                    #pragma warning disable CS8604

                    #if NET7_0_OR_GREATER
                    [global::System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
                    #endif
                    public NestedInStructWithGenerics()
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

    private const string NestedInStructWithGenericsWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class OutterNestedInStructWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> WithAge<TOutter, TInner>(this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject, int value)
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

    private const string NestedInStructWithGenericsWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class OutterNestedInStructWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> WithAttributes<TOutter, TInner>(this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> WithAttributes<TOutter, TInner>(this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject, object instance, global::System.Func<object, object> builder)
                    where TOutter : class
                    where TInner : struct
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> WithAttributes<TOutter, TInner>(this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject, global::System.Func<object, object> builder)
                    where TOutter : class
                    where TInner : struct
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> WithAttributes<TOutter, TInner>(this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject, params object[] values)
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

    private const string NestedInStructWithGenericsWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class OutterNestedInStructWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> WithName<TOutter, TInner>(this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject, string value)
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