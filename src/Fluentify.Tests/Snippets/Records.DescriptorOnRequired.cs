namespace Fluentify.Snippets;

public static partial class Records
{
    public static readonly Declared DescriptorOnRequired;

    public static readonly Generated DescriptorOnRequiredConstructor = new(
        DescriptorOnRequiredConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.DescriptorOnRequired.ctor");

    public static readonly Generated DescriptorOnRequiredAgedExtensions = new(
        DescriptorOnRequiredAgedExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.DescriptorOnRequiredExtensions.Aged");

    public static readonly Generated DescriptorOnRequiredWithAttributesExtensions = new(
        DescriptorOnRequiredWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.DescriptorOnRequiredExtensions.WithAttributes");

    public static readonly Generated DescriptorOnRequiredWithNameExtensions = new(
        DescriptorOnRequiredWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.DescriptorOnRequiredExtensions.WithName");

    private const string DescriptorOnRequiredContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record DescriptorOnRequired([Descriptor("Aged")] int Age, string Name, IReadOnlyList<object>? Attributes = default);
        }
        """;

    private const string DescriptorOnRequiredConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial record DescriptorOnRequired
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public DescriptorOnRequired()
                    : this(default, default, default)
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string DescriptorOnRequiredAgedExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class DescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Records.Testing.DescriptorOnRequired Aged(
                    this global::Fluentify.Records.Testing.DescriptorOnRequired subject,
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

    private const string DescriptorOnRequiredWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class DescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Records.Testing.DescriptorOnRequired WithAttributes(
                    this global::Fluentify.Records.Testing.DescriptorOnRequired subject,
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

                public static global::Fluentify.Records.Testing.DescriptorOnRequired WithAttributes(
                    this global::Fluentify.Records.Testing.DescriptorOnRequired subject,
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

    private const string DescriptorOnRequiredWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class DescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Records.Testing.DescriptorOnRequired WithName(
                    this global::Fluentify.Records.Testing.DescriptorOnRequired subject,
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