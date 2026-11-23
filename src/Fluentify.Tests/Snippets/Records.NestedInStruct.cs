namespace Fluentify.Snippets;

public static partial class Records
{
    public const string NestedInStructContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            public partial struct Outter
            {
                [Fluentify]
                public sealed partial record NestedInStruct(int Age, string Name, IReadOnlyList<object>? Attributes = default);
            }
        }
        """;

    public static readonly Declared NestedInStruct;

    public static readonly Generated NestedInStructConstructor = new(
        NestedInStructConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInStruct.ctor");

    public static readonly Generated NestedInStructWithAgeExtensions = new(
        NestedInStructWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInStructExtensions.WithAge");

    public static readonly Generated NestedInStructWithAttributesExtensions = new(
        NestedInStructWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInStructExtensions.WithAttributes");

    public static readonly Generated NestedInStructWithNameExtensions = new(
        NestedInStructWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInStructExtensions.WithName");

    private const string NestedInStructConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            partial struct Outter
            {
                partial record NestedInStruct
                {
                    #pragma warning disable CS8604

                    #if NET7_0_OR_GREATER
                    [global::System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
                    #endif
                    public NestedInStruct()
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

    private const string NestedInStructWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class OutterNestedInStructExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInStruct WithAge(this global::Fluentify.Records.Testing.Outter.NestedInStruct subject, int value)
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

    private const string NestedInStructWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class OutterNestedInStructExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInStruct WithAttributes(this global::Fluentify.Records.Testing.Outter.NestedInStruct subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Records.Testing.Outter.NestedInStruct WithAttributes(this global::Fluentify.Records.Testing.Outter.NestedInStruct subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.Outter.NestedInStruct WithAttributes(this global::Fluentify.Records.Testing.Outter.NestedInStruct subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.Outter.NestedInStruct WithAttributes(this global::Fluentify.Records.Testing.Outter.NestedInStruct subject, params object[] values)
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

    private const string NestedInStructWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class OutterNestedInStructExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInStruct WithName(this global::Fluentify.Records.Testing.Outter.NestedInStruct subject, string value)
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