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
        "Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner>.ctor");

    public static readonly Generated NestedInRecordWithGenericsWithAgeExtensions = new(
        NestedInRecordWithGenericsWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner>Extensions.WithAge");

    public static readonly Generated NestedInRecordWithGenericsWithAttributesExtensions = new(
        NestedInRecordWithGenericsWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner>Extensions.WithAttributes");

    public static readonly Generated NestedInRecordWithGenericsWithNameExtensions = new(
        NestedInRecordWithGenericsWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner>Extensions.WithName");

    private const string NestedInRecordWithGenericsConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial record Outter<TOutter>
            {
                partial record NestedInRecordWithGenerics<TInner>
                {
                    #pragma warning disable CS8604

                    #if NET7_0_OR_GREATER
                    [SetsRequiredMembers]
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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInRecordWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner> WithAge(
                    this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner> subject,
                    int value)
                    where TOutter : class
                    where TInner : struct
                {
                    subject.ThrowIfNull("subject");

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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInRecordWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner> WithAttributes(
                    this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner> subject,
                    params object[] values)
                    where TOutter : class
                    where TInner : struct
                {
                    subject.ThrowIfNull("subject");

                    global::System.Collections.Generic.IReadOnlyList<object>? value = values;

                    if (subject.Attributes != null)
                    {
                        value = subject.Attributes
                            .Union(values)
                            .ToArray();
                    }

                    return subject with
                    {
                        Attributes = value,
                    };
                }

                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner> WithAttributes(
                    this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner> subject,
                    Func<object, object> builder)
                    where TOutter : class
                    where TInner : struct
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInRecordWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner> WithName(
                    this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInRecordWithGenerics<TInner> subject,
                    string value)
                    where TOutter : class
                    where TInner : struct
                {
                    subject.ThrowIfNull("subject");

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