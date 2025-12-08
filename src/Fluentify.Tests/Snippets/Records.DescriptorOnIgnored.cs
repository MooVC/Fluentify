namespace Fluentify.Snippets;

public static partial class Records
{
    public const string DescriptorOnIgnoredContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record DescriptorOnIgnored(int Age, [Descriptor("Named"), Ignore] string Name, IReadOnlyList<object>? Attributes = default);
        }
        """;

    public static readonly Declared DescriptorOnIgnored;

    public static readonly Generated DescriptorOnIgnoredConstructor = new(
        DescriptorOnIgnoredConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.DescriptorOnIgnored.ctor");

    public static readonly Generated DescriptorOnIgnoredWithAgeExtensions = new(
        DescriptorOnIgnoredWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.DescriptorOnIgnoredExtensions.WithAge");

    public static readonly Generated DescriptorOnIgnoredWithAttributesExtensions = new(
        DescriptorOnIgnoredWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.DescriptorOnIgnoredExtensions.WithAttributes");

    private const string DescriptorOnIgnoredConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial record DescriptorOnIgnored
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public DescriptorOnIgnored()
                    : this(default(int), default(string), default(global::System.Collections.Generic.IReadOnlyList<object>?))
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string DescriptorOnIgnoredWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class DescriptorOnIgnoredExtensions
            {
                public static global::Fluentify.Records.Testing.DescriptorOnIgnored WithAge(
                    this global::Fluentify.Records.Testing.DescriptorOnIgnored subject,
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

    private const string DescriptorOnIgnoredWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class DescriptorOnIgnoredExtensions
            {
                public static global::Fluentify.Records.Testing.DescriptorOnIgnored WithAttributes(
                    this global::Fluentify.Records.Testing.DescriptorOnIgnored subject,
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

                public static global::Fluentify.Records.Testing.DescriptorOnIgnored WithAttributes(
                    this global::Fluentify.Records.Testing.DescriptorOnIgnored subject,
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
}