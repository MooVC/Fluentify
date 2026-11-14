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
            partial record SelfDescriptorOnOptional
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [global::System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
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
            public static partial class SelfDescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Records.Testing.SelfDescriptorOnOptional WithAge(this global::Fluentify.Records.Testing.SelfDescriptorOnOptional subject, int value)
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

    private const string SelfDescriptorOnOptionalAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class SelfDescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Records.Testing.SelfDescriptorOnOptional Attributes(this global::Fluentify.Records.Testing.SelfDescriptorOnOptional subject, global::System.Func<object, object> builder, params object[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    foreach (var value in values)
                    {
                        subject = subject.Attributes(value, builder);
                    }

                    return subject;
                }

                public static global::Fluentify.Records.Testing.SelfDescriptorOnOptional Attributes(this global::Fluentify.Records.Testing.SelfDescriptorOnOptional subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.Attributes(instance);
                }

                public static global::Fluentify.Records.Testing.SelfDescriptorOnOptional Attributes(this global::Fluentify.Records.Testing.SelfDescriptorOnOptional subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.Attributes(instance);
                }

                public static global::Fluentify.Records.Testing.SelfDescriptorOnOptional Attributes(this global::Fluentify.Records.Testing.SelfDescriptorOnOptional subject, params object[] values)
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

    private const string SelfDescriptorOnOptionalWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class SelfDescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Records.Testing.SelfDescriptorOnOptional WithName(this global::Fluentify.Records.Testing.SelfDescriptorOnOptional subject, string value)
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