namespace Fluentify.Snippets;

public static partial class Records
{
    public const string SelfDescriptorOnRequiredContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record SelfDescriptorOnRequired(
                [Descriptor] int Age,
                [Descriptor("")] string Name,
                IReadOnlyList<object>? Attributes = default);
        }
        """;

    public static readonly Declared SelfDescriptorOnRequired;

    public static readonly Generated SelfDescriptorOnRequiredConstructor = new(
        SelfDescriptorOnRequiredConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SelfDescriptorOnRequired.ctor");

    public static readonly Generated SelfDescriptorOnRequiredAgeExtensions = new(
        SelfDescriptorOnRequiredAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SelfDescriptorOnRequiredExtensions.Age");

    public static readonly Generated SelfDescriptorOnRequiredWithAttributesExtensions = new(
        SelfDescriptorOnRequiredWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SelfDescriptorOnRequiredExtensions.WithAttributes");

    public static readonly Generated SelfDescriptorOnRequiredNameExtensions = new(
        SelfDescriptorOnRequiredNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SelfDescriptorOnRequiredExtensions.Name");

    private const string SelfDescriptorOnRequiredConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial record SelfDescriptorOnRequired
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public SelfDescriptorOnRequired()
                    : this(default(int), default(string), default(global::System.Collections.Generic.IReadOnlyList<object>?))
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string SelfDescriptorOnRequiredAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SelfDescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Records.Testing.SelfDescriptorOnRequired Age(
                    this global::Fluentify.Records.Testing.SelfDescriptorOnRequired subject,
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

    private const string SelfDescriptorOnRequiredWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SelfDescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Records.Testing.SelfDescriptorOnRequired WithAttributes(
                    this global::Fluentify.Records.Testing.SelfDescriptorOnRequired subject,
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

    private const string SelfDescriptorOnRequiredNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SelfDescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Records.Testing.SelfDescriptorOnRequired Name(
                    this global::Fluentify.Records.Testing.SelfDescriptorOnRequired subject,
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