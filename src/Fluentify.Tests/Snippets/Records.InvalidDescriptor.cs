namespace Fluentify.Snippets;

public static partial class Records
{
    public const string InvalidDescriptorContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record InvalidDescriptor([Descriptor(" ")] int Age, string Name, IReadOnlyList<object>? Attributes = default);
        }
        """;

    public static readonly Declared InvalidDescriptor;

    public static readonly Generated InvalidDescriptorConstructor = new(
        InvalidDescriptorConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.InvalidDescriptor.ctor");

    public static readonly Generated InvalidDescriptorWithAgeExtensions = new(
        InvalidDescriptorWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.InvalidDescriptorExtensions.WithAge");

    public static readonly Generated InvalidDescriptorWithAttributesExtensions = new(
        InvalidDescriptorWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.InvalidDescriptorExtensions.WithAttributes");

    public static readonly Generated InvalidDescriptorWithNameExtensions = new(
        InvalidDescriptorWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.InvalidDescriptorExtensions.WithName");

    private const string InvalidDescriptorConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial record InvalidDescriptor
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public InvalidDescriptor()
                    : this(default(int), default(string), default(global::System.Collections.Generic.IReadOnlyList<object>?))
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string InvalidDescriptorWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class InvalidDescriptorExtensions
            {
                public static global::Fluentify.Records.Testing.InvalidDescriptor WithAge(
                    this global::Fluentify.Records.Testing.InvalidDescriptor subject,
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

    private const string InvalidDescriptorWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class InvalidDescriptorExtensions
            {
                public static global::Fluentify.Records.Testing.InvalidDescriptor WithAttributes(
                    this global::Fluentify.Records.Testing.InvalidDescriptor subject,
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

                public static global::Fluentify.Records.Testing.InvalidDescriptor WithAttributes(
                    this global::Fluentify.Records.Testing.InvalidDescriptor subject,
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

    private const string InvalidDescriptorWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class InvalidDescriptorExtensions
            {
                public static global::Fluentify.Records.Testing.InvalidDescriptor WithName(
                    this global::Fluentify.Records.Testing.InvalidDescriptor subject,
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