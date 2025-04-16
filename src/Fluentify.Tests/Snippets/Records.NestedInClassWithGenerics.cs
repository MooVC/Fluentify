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
        "Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner>.ctor");

    public static readonly Generated NestedInClassWithGenericsWithAgeExtensions = new(
        NestedInClassWithGenericsWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner>Extensions.WithAge");

    public static readonly Generated NestedInClassWithGenericsWithAttributesExtensions = new(
        NestedInClassWithGenericsWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner>Extensions.WithAttributes");

    public static readonly Generated NestedInClassWithGenericsWithNameExtensions = new(
        NestedInClassWithGenericsWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner>Extensions.WithName");

    private const string NestedInClassWithGenericsConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial class Outter<TOutter>
            {
                partial record NestedInClassWithGenerics<TInner>
                {
                    #pragma warning disable CS8604

                    #if NET7_0_OR_GREATER
                    [SetsRequiredMembers]
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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInClassWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> WithAge(
                    this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> subject,
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

    private const string NestedInClassWithGenericsWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInClassWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> WithAttributes(
                    this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> subject,
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

                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> WithAttributes(
                    this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> subject,
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

    private const string NestedInClassWithGenericsWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInClassWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> WithName(
                    this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> subject,
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