namespace Fluentify.Snippets;

public static partial class Records
{
    public const string SelfDescriptorOnIgnoredContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record SelfDescriptorOnIgnored(int Age, [Descriptor, Ignore] string Name, IReadOnlyList<object>? Attributes = default);
        }
        """;

    public static readonly Declared SelfDescriptorOnIgnored;

    public static readonly Generated SelfDescriptorOnIgnoredConstructor = new(
        SelfDescriptorOnIgnoredConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SelfDescriptorOnIgnored.ctor");

    public static readonly Generated SelfDescriptorOnIgnoredWithAgeExtensions = new(
        SelfDescriptorOnIgnoredWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SelfDescriptorOnIgnoredExtensions.WithAge");

    public static readonly Generated SelfDescriptorOnIgnoredWithAttributesExtensions = new(
        SelfDescriptorOnIgnoredWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SelfDescriptorOnIgnoredExtensions.WithAttributes");

    private const string SelfDescriptorOnIgnoredConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial record SelfDescriptorOnIgnored
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public SelfDescriptorOnIgnored()
                    : this(default(int), default(string), default(global::System.Collections.Generic.IReadOnlyList<object>?))
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string SelfDescriptorOnIgnoredWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SelfDescriptorOnIgnoredExtensions
            {
                public static global::Fluentify.Records.Testing.SelfDescriptorOnIgnored WithAge(
                    this global::Fluentify.Records.Testing.SelfDescriptorOnIgnored subject,
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

    private const string SelfDescriptorOnIgnoredWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SelfDescriptorOnIgnoredExtensions
            {
                public static global::Fluentify.Records.Testing.SelfDescriptorOnIgnored WithAttributes(
                    this global::Fluentify.Records.Testing.SelfDescriptorOnIgnored subject,
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

            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;
}