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
            partial record SelfDescriptorOnRequired
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [global::System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
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
            public static partial class SelfDescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Records.Testing.SelfDescriptorOnRequired Age(this global::Fluentify.Records.Testing.SelfDescriptorOnRequired subject, int value)
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

    private const string SelfDescriptorOnRequiredWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class SelfDescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Records.Testing.SelfDescriptorOnRequired WithAttributes(this global::Fluentify.Records.Testing.SelfDescriptorOnRequired subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Records.Testing.SelfDescriptorOnRequired WithAttributes(this global::Fluentify.Records.Testing.SelfDescriptorOnRequired subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.SelfDescriptorOnRequired WithAttributes(this global::Fluentify.Records.Testing.SelfDescriptorOnRequired subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.SelfDescriptorOnRequired WithAttributes(this global::Fluentify.Records.Testing.SelfDescriptorOnRequired subject, params object[] values)
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

    private const string SelfDescriptorOnRequiredNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class SelfDescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Records.Testing.SelfDescriptorOnRequired Name(this global::Fluentify.Records.Testing.SelfDescriptorOnRequired subject, string value)
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