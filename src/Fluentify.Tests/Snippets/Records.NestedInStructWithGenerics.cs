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
            using System.Diagnostics.CodeAnalysis;

            partial struct Outter<TOutter>
            {
                partial record NestedInStructWithGenerics<TInner>
                {
                    #pragma warning disable CS8604

                    #if NET7_0_OR_GREATER
                    [SetsRequiredMembers]
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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInStructWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> WithAge<TOutter, TInner>(
                    this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject,
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

    private const string NestedInStructWithGenericsWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInStructWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> WithAttributes<TOutter, TInner>(
                    this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject,
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

                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> WithAttributes<TOutter, TInner>(
                    this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject,
                    Func<object, object> builder)
                    where TOutter : class
                    where TInner : struct
                {
                    subject.ThrowIfNull("subject");
                    builder.ThrowIfNull("builder");

                    var instance = subject.Attributes?.FirstOrDefault();

                    if (instance is null)
                    {
                        instance = new object();
                    }

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInStructWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> WithName<TOutter, TInner>(
                    this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject,
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