namespace Fluentify.Snippets;

public static partial class Records
{
    public const string SelfDescriptorOnOptionalContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record SelfDescriptorOnOptional(int Age, string Name, [Descriptor] IReadOnlyList<object>? Attributes = default);
        }
        """;

    public static readonly Declared SelfDescriptorOnOptional;

    public static readonly Generated SelfDescriptorOnOptionalConstructor = new(
        SelfDescriptorOnOptionalConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SelfDescriptorOnOptional.ctor");

    public static readonly Generated SelfDescriptorOnOptionalWithAgeExtensions = new(
        SelfDescriptorOnOptionalWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SelfDescriptorOnOptionalExtensions.WithAge");

    public static readonly Generated SelfDescriptorOnOptionalAttributesExtensions = new(
        SelfDescriptorOnOptionalAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SelfDescriptorOnOptionalExtensions.Attributes");

    public static readonly Generated SelfDescriptorOnOptionalWithNameExtensions = new(
        SelfDescriptorOnOptionalWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SelfDescriptorOnOptionalExtensions.WithName");

    private const string SelfDescriptorOnOptionalConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial record SelfDescriptorOnOptional
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public SelfDescriptorOnOptional()
                    : this(default(int), default(string), default(global::System.Collections.Generic.IReadOnlyList<object>?))
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string SelfDescriptorOnOptionalWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SelfDescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Records.Testing.SelfDescriptorOnOptional WithAge(
                    this global::Fluentify.Records.Testing.SelfDescriptorOnOptional subject,
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

    private const string SelfDescriptorOnOptionalAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SelfDescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Records.Testing.SelfDescriptorOnOptional Attributes(
                    this global::Fluentify.Records.Testing.SelfDescriptorOnOptional subject,
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

    private const string SelfDescriptorOnOptionalWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SelfDescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Records.Testing.SelfDescriptorOnOptional WithName(
                    this global::Fluentify.Records.Testing.SelfDescriptorOnOptional subject,
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