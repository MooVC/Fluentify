namespace Fluentify.Snippets;

public static partial class Records
{
    public const string SingleGenericContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections;

            [Fluentify]
            public sealed partial record SingleGeneric<T>(int Age, string Name, T? Attributes = default)
                where T : IEnumerable;
        }
        """;

    public static readonly Declared SingleGeneric;

    public static readonly Generated SingleGenericConstructor = new(
        SingleGenericConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SingleGeneric.ctor");

    public static readonly Generated SingleGenericWithAgeExtensions = new(
        SingleGenericWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SingleGenericExtensions.WithAge");

    public static readonly Generated SingleGenericWithAttributesExtensions = new(
        SingleGenericWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SingleGenericExtensions.WithAttributes");

    public static readonly Generated SingleGenericWithNameExtensions = new(
        SingleGenericWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SingleGenericExtensions.WithName");

    private const string SingleGenericConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            partial record SingleGeneric<T>
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [global::System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
                #endif
                public SingleGeneric()
                    : this(default(int), default(string), default(T?))
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string SingleGenericWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class SingleGenericExtensions
            {
                public static global::Fluentify.Records.Testing.SingleGeneric<T> WithAge<T>(this global::Fluentify.Records.Testing.SingleGeneric<T> subject, int value)
                    where T : global::System.Collections.IEnumerable
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

    private const string SingleGenericWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class SingleGenericExtensions
            {
                public static global::Fluentify.Records.Testing.SingleGeneric<T> WithAttributes<T>(this global::Fluentify.Records.Testing.SingleGeneric<T> subject, T? instance, global::System.Func<T?, T?> builder)
                    where T : global::System.Collections.IEnumerable
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.SingleGeneric<T> WithAttributes<T>(this global::Fluentify.Records.Testing.SingleGeneric<T> subject, global::System.Func<T?, T?> builder)
                    where T : global::System.Collections.IEnumerable
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

                public static global::Fluentify.Records.Testing.SingleGeneric<T> WithAttributes<T>(this global::Fluentify.Records.Testing.SingleGeneric<T> subject, T? value)
                    where T : global::System.Collections.IEnumerable
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

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

    private const string SingleGenericWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class SingleGenericExtensions
            {
                public static global::Fluentify.Records.Testing.SingleGeneric<T> WithName<T>(this global::Fluentify.Records.Testing.SingleGeneric<T> subject, string value)
                    where T : global::System.Collections.IEnumerable
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