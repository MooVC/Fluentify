namespace Fluentify.Snippets;

public static partial class Records
{
    public const string InvalidDescriptorContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record InvalidDescriptor(
                [Descriptor(" ")] int Age,
                [Descriptor("With Name")] string Name,
                [Descriptor("&Invalid")] IReadOnlyList<object>? Attributes = default);
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
            partial record InvalidDescriptor
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [global::System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
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
            public static partial class InvalidDescriptorExtensions
            {
                public static global::Fluentify.Records.Testing.InvalidDescriptor WithAge(this global::Fluentify.Records.Testing.InvalidDescriptor subject, int value)
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

    private const string InvalidDescriptorWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class InvalidDescriptorExtensions
            {
                public static global::Fluentify.Records.Testing.InvalidDescriptor WithAttributes(this global::Fluentify.Records.Testing.InvalidDescriptor subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Records.Testing.InvalidDescriptor WithAttributes(this global::Fluentify.Records.Testing.InvalidDescriptor subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.InvalidDescriptor WithAttributes(this global::Fluentify.Records.Testing.InvalidDescriptor subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.InvalidDescriptor WithAttributes(this global::Fluentify.Records.Testing.InvalidDescriptor subject, params object[] values)
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

    private const string InvalidDescriptorWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class InvalidDescriptorExtensions
            {
                public static global::Fluentify.Records.Testing.InvalidDescriptor WithName(this global::Fluentify.Records.Testing.InvalidDescriptor subject, string value)
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