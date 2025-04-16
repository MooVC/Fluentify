namespace Fluentify.Snippets;

public static partial class Records
{
    public const string NestedInInterfaceWithGenericsContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            public partial interface Outter<TOutter>
                where TOutter : class
            {
                [Fluentify]
                public sealed partial record NestedInInterfaceWithGenerics<TInner>(int Age, string Name, IReadOnlyList<object>? Attributes = default)
                    where TInner : struct;
            }
        }
        """;

    public static readonly Declared NestedInInterfaceWithGenerics;

    public static readonly Generated NestedInInterfaceWithGenericsConstructor = new(
        NestedInInterfaceWithGenericsConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter<TOutter>.NestedInInterfaceWithGenerics<TInner>.ctor");

    public static readonly Generated NestedInInterfaceWithGenericsWithAgeExtensions = new(
        NestedInInterfaceWithGenericsWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter<TOutter>.NestedInInterfaceWithGenerics<TInner>Extensions.WithAge");

    public static readonly Generated NestedInInterfaceWithGenericsWithAttributesExtensions = new(
        NestedInInterfaceWithGenericsWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter<TOutter>.NestedInInterfaceWithGenerics<TInner>Extensions.WithAttributes");

    public static readonly Generated NestedInInterfaceWithGenericsWithNameExtensions = new(
        NestedInInterfaceWithGenericsWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter<TOutter>.NestedInInterfaceWithGenerics<TInner>Extensions.WithName");

    private const string NestedInInterfaceWithGenericsConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial interface Outter<TOutter>
            {
                partial record NestedInInterfaceWithGenerics<TInner>
                {
                    #pragma warning disable CS8604

                    #if NET7_0_OR_GREATER
                    [SetsRequiredMembers]
                    #endif
                    public NestedInInterfaceWithGenerics()
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

    private const string NestedInInterfaceWithGenericsWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInInterfaceWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInInterfaceWithGenerics<TInner> WithAge(
                    this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInInterfaceWithGenerics<TInner> subject,
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

    private const string NestedInInterfaceWithGenericsWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInInterfaceWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInInterfaceWithGenerics<TInner> WithAttributes(
                    this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInInterfaceWithGenerics<TInner> subject,
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

                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInInterfaceWithGenerics<TInner> WithAttributes(
                    this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInInterfaceWithGenerics<TInner> subject,
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

    private const string NestedInInterfaceWithGenericsWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInInterfaceWithGenericsExtensions
            {
                public static global::Fluentify.Records.Testing.Outter<TOutter>.NestedInInterfaceWithGenerics<TInner> WithName(
                    this global::Fluentify.Records.Testing.Outter<TOutter>.NestedInInterfaceWithGenerics<TInner> subject,
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