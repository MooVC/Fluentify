namespace Fluentify.Snippets;

public static partial class Records
{
    public const string NestedInRecordContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            public sealed partial record Outter
            {
                [Fluentify]
                public sealed partial record NestedInRecord(int Age, string Name, IReadOnlyList<object>? Attributes = default);
            }
        }
        """;

    public static readonly Declared NestedInRecord;

    public static readonly Generated NestedInRecordConstructor = new(
        NestedInRecordConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInRecord.ctor");

    public static readonly Generated NestedInRecordWithAgeExtensions = new(
        NestedInRecordWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInRecordExtensions.WithAge");

    public static readonly Generated NestedInRecordWithAttributesExtensions = new(
        NestedInRecordWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInRecordExtensions.WithAttributes");

    public static readonly Generated NestedInRecordWithNameExtensions = new(
        NestedInRecordWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Outter.NestedInRecordExtensions.WithName");

    private const string NestedInRecordConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            partial record Outter
            {
                partial record NestedInRecord
                {
                    #pragma warning disable CS8604

                    #if NET7_0_OR_GREATER
                    [global::System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
                    #endif
                    public NestedInRecord()
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

    private const string NestedInRecordWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class OutterNestedInRecordExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInRecord WithAge(this global::Fluentify.Records.Testing.Outter.NestedInRecord subject, int value)
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

    private const string NestedInRecordWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class OutterNestedInRecordExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInRecord WithAttributes(this global::Fluentify.Records.Testing.Outter.NestedInRecord subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Records.Testing.Outter.NestedInRecord WithAttributes(this global::Fluentify.Records.Testing.Outter.NestedInRecord subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.Outter.NestedInRecord WithAttributes(this global::Fluentify.Records.Testing.Outter.NestedInRecord subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.Outter.NestedInRecord WithAttributes(this global::Fluentify.Records.Testing.Outter.NestedInRecord subject, params object[] values)
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

    private const string NestedInRecordWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class OutterNestedInRecordExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInRecord WithName(this global::Fluentify.Records.Testing.Outter.NestedInRecord subject, string value)
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