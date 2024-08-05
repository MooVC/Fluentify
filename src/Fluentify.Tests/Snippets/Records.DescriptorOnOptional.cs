namespace Fluentify.Snippets;

public static partial class Records
{
    public static readonly Declared DescriptorOnOptional;

    public static readonly Generated DescriptorOnOptionalConstructor = new(
        DescriptorOnOptionalConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.DescriptorOnOptional.ctor");

    public static readonly Generated DescriptorOnOptionalWithAgeExtensions = new(
        DescriptorOnOptionalWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.DescriptorOnOptionalExtensions.WithAge");

    public static readonly Generated DescriptorOnOptionalAttributedWithExtensions = new(
        DescriptorOnOptionalAttributedWithExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.DescriptorOnOptionalExtensions.AttributedWith");

    public static readonly Generated DescriptorOnOptionalWithNameExtensions = new(
        DescriptorOnOptionalWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.DescriptorOnOptionalExtensions.WithName");

    private const string DescriptorOnOptionalContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record DescriptorOnOptional(int Age, string Name, [Descriptor("AttributedWith")] IReadOnlyList<object>? Attributes = default);
        }
        """;

    private const string DescriptorOnOptionalConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial record DescriptorOnOptional
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public DescriptorOnOptional()
                    : this(default, default, default)
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string DescriptorOnOptionalWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class DescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Records.Testing.DescriptorOnOptional WithAge(
                    this global::Fluentify.Records.Testing.DescriptorOnOptional subject,
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

    private const string DescriptorOnOptionalAttributedWithExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class DescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Records.Testing.DescriptorOnOptional AttributedWith(
                    this global::Fluentify.Records.Testing.DescriptorOnOptional subject,
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

                public static global::Fluentify.Records.Testing.DescriptorOnOptional AttributedWith(
                    this global::Fluentify.Records.Testing.DescriptorOnOptional subject,
                    Func<object, object> builder)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.AttributedWith(instance);
                }
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string DescriptorOnOptionalWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class DescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Records.Testing.DescriptorOnOptional WithName(
                    this global::Fluentify.Records.Testing.DescriptorOnOptional subject,
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