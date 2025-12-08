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
            using System.Diagnostics.CodeAnalysis;

            partial record Outter
            {
                partial record NestedInRecord
                {
                    #pragma warning disable CS8604

                    #if NET7_0_OR_GREATER
                    [SetsRequiredMembers]
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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInRecordExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInRecord WithAge(
                    this global::Fluentify.Records.Testing.Outter.NestedInRecord subject,
                    int value)
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

    private const string NestedInRecordWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInRecordExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInRecord WithAttributes(
                    this global::Fluentify.Records.Testing.Outter.NestedInRecord subject,
                    params object[] values)
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

                public static global::Fluentify.Records.Testing.Outter.NestedInRecord WithAttributes(
                    this global::Fluentify.Records.Testing.Outter.NestedInRecord subject,
                    Func<object, object> builder)
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

    private const string NestedInRecordWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInRecordExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInRecord WithName(
                    this global::Fluentify.Records.Testing.Outter.NestedInRecord subject,
                    string value)
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