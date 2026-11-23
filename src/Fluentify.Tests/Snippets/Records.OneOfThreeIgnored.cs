namespace Fluentify.Snippets;

public static partial class Records
{
    public const string OneOfThreeIgnoredContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record OneOfThreeIgnored(int Age, [Ignore] string Name, IReadOnlyList<object>? Attributes = default);
        }
        """;

    public static readonly Declared OneOfThreeIgnored;

    public static readonly Generated OneOfThreeIgnoredConstructor = new(
        OneOfThreeIgnoredConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.OneOfThreeIgnored.ctor");

    public static readonly Generated OneOfThreeIgnoredWithAgeExtensions = new(
        OneOfThreeIgnoredWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.OneOfThreeIgnoredExtensions.WithAge");

    public static readonly Generated OneOfThreeIgnoredWithAttributesExtensions = new(
        OneOfThreeIgnoredWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.OneOfThreeIgnoredExtensions.WithAttributes");

    private const string OneOfThreeIgnoredConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            partial record OneOfThreeIgnored
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [global::System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
                #endif
                public OneOfThreeIgnored()
                    : this(default(int), default(string), default(global::System.Collections.Generic.IReadOnlyList<object>?))
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string OneOfThreeIgnoredWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class OneOfThreeIgnoredExtensions
            {
                public static global::Fluentify.Records.Testing.OneOfThreeIgnored WithAge(this global::Fluentify.Records.Testing.OneOfThreeIgnored subject, int value)
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

    private const string OneOfThreeIgnoredWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class OneOfThreeIgnoredExtensions
            {
                public static global::Fluentify.Records.Testing.OneOfThreeIgnored WithAttributes(this global::Fluentify.Records.Testing.OneOfThreeIgnored subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Records.Testing.OneOfThreeIgnored WithAttributes(this global::Fluentify.Records.Testing.OneOfThreeIgnored subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.OneOfThreeIgnored WithAttributes(this global::Fluentify.Records.Testing.OneOfThreeIgnored subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.OneOfThreeIgnored WithAttributes(this global::Fluentify.Records.Testing.OneOfThreeIgnored subject, params object[] values)
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