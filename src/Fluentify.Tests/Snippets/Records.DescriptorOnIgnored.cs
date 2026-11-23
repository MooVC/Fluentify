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
            partial record DescriptorOnIgnored
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [global::System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
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
            public static partial class DescriptorOnIgnoredExtensions
            {
                public static global::Fluentify.Records.Testing.DescriptorOnIgnored WithAge(this global::Fluentify.Records.Testing.DescriptorOnIgnored subject, int value)
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

    private const string DescriptorOnIgnoredWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class DescriptorOnIgnoredExtensions
            {
                public static global::Fluentify.Records.Testing.DescriptorOnIgnored WithAttributes(this global::Fluentify.Records.Testing.DescriptorOnIgnored subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Records.Testing.DescriptorOnIgnored WithAttributes(this global::Fluentify.Records.Testing.DescriptorOnIgnored subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.DescriptorOnIgnored WithAttributes(this global::Fluentify.Records.Testing.DescriptorOnIgnored subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.DescriptorOnIgnored WithAttributes(this global::Fluentify.Records.Testing.DescriptorOnIgnored subject, params object[] values)
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
}