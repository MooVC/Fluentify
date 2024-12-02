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
            using System.Diagnostics.CodeAnalysis;

            partial struct Outter
            {
                partial record NestedInStruct
                {
                    #pragma warning disable CS8604

                    #if NET7_0_OR_GREATER
                    [SetsRequiredMembers]
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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInStructExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInStruct WithAge(
                    this global::Fluentify.Records.Testing.Outter.NestedInStruct subject,
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

    private const string NestedInStructWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInStructExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInStruct WithAttributes(
                    this global::Fluentify.Records.Testing.Outter.NestedInStruct subject,
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

                public static global::Fluentify.Records.Testing.Outter.NestedInStruct WithAttributes(
                    this global::Fluentify.Records.Testing.Outter.NestedInStruct subject,
                    Func<object, object> builder)
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

    private const string NestedInStructWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class NestedInStructExtensions
            {
                public static global::Fluentify.Records.Testing.Outter.NestedInStruct WithName(
                    this global::Fluentify.Records.Testing.Outter.NestedInStruct subject,
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