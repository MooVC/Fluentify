namespace Fluentify.Snippets;

public static partial class Records
{
    public const string DescriptorOnOptionalContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record DescriptorOnOptional(int Age, string Name, [Descriptor("AttributedWith")] IReadOnlyList<object>? Attributes = default);
        }
        """;

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

    private const string DescriptorOnOptionalConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            partial record DescriptorOnOptional
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [global::System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
                #endif
                public DescriptorOnOptional()
                    : this(default(int), default(string), default(global::System.Collections.Generic.IReadOnlyList<object>?))
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
            public static partial class DescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Records.Testing.DescriptorOnOptional WithAge(this global::Fluentify.Records.Testing.DescriptorOnOptional subject, int value)
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

    private const string DescriptorOnOptionalAttributedWithExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class DescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Records.Testing.DescriptorOnOptional AttributedWith(this global::Fluentify.Records.Testing.DescriptorOnOptional subject, global::System.Func<object, object> builder, params object[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    foreach (var value in values)
                    {
                        subject = subject.AttributedWith(value, builder);
                    }

                    return subject;
                }

                public static global::Fluentify.Records.Testing.DescriptorOnOptional AttributedWith(this global::Fluentify.Records.Testing.DescriptorOnOptional subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.AttributedWith(instance);
                }

                public static global::Fluentify.Records.Testing.DescriptorOnOptional AttributedWith(this global::Fluentify.Records.Testing.DescriptorOnOptional subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.AttributedWith(instance);
                }

                public static global::Fluentify.Records.Testing.DescriptorOnOptional AttributedWith(this global::Fluentify.Records.Testing.DescriptorOnOptional subject, params object[] values)
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

    private const string DescriptorOnOptionalWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class DescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Records.Testing.DescriptorOnOptional WithName(this global::Fluentify.Records.Testing.DescriptorOnOptional subject, string value)
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