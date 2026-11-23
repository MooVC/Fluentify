namespace Fluentify.Snippets;

public static partial class Records
{
    public const string NestedInRecordWithGenericsContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            public sealed partial record Outter<TOutter>
                where TOutter : class
            {
                [Fluentify]
                public sealed partial record NestedInRecordWithGenerics<TInner>(int Age, string Name, IReadOnlyList<object>? Attributes = default)
                    where TInner : struct;
            }
        }
        """;

    public static readonly Declared NestedInRecordWithGenerics;

    public static readonly Generated NestedInRecordWithGenericsConstructor = new(
        NestedInRecordWithGenericsConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInRecordWithGenerics.ctor");

    public static readonly Generated NestedInRecordWithGenericsWithAgeExtensions = new(
        NestedInRecordWithGenericsWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInRecordWithGenericsExtensions.WithAge");

    public static readonly Generated NestedInRecordWithGenericsWithAttributesExtensions = new(
        NestedInRecordWithGenericsWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInRecordWithGenericsExtensions.WithAttributes");

    public static readonly Generated NestedInRecordWithGenericsWithNameExtensions = new(
        NestedInRecordWithGenericsWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInRecordWithGenericsExtensions.WithName");

    private const string NestedInRecordWithGenericsConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            partial record Outter<TOutter>
            {
                partial record NestedInRecordWithGenerics<TInner>
                {
                    #pragma warning disable CS8604

                    #if NET7_0_OR_GREATER
                    [global::System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
                    #endif
                    public NestedInRecordWithGenerics()
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

    private const string NestedInRecordWithGenericsWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class OutterNestedInRecordWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner> WithAge<TOutter, TInner>(this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner> subject, int value)
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

    private const string NestedInRecordWithGenericsWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class OutterNestedInRecordWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner> WithAttributes<TOutter, TInner>(this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner> subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner> WithAttributes<TOutter, TInner>(this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner> subject, object instance, global::System.Func<object, object> builder)
                    where TOutter : class
                    where TInner : struct
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner> WithAttributes<TOutter, TInner>(this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner> subject, global::System.Func<object, object> builder)
                    where TOutter : class
                    where TInner : struct
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner> WithAttributes<TOutter, TInner>(this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner> subject, params object[] values)
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

    private const string NestedInRecordWithGenericsWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class OutterNestedInRecordWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner> WithName<TOutter, TInner>(this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner> subject, string value)
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