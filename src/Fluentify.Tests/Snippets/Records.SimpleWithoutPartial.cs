namespace Fluentify.Snippets;

public static partial class Records
{
    public const string SimpleWithoutPartialContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed record SimpleWithoutPartial(int Age, string Name, IReadOnlyList<object>? Attributes = default);
        }
        """;

    public static readonly Declared SimpleWithoutPartial;

    public static readonly Generated SimpleWithoutPartialWithAgeExtensions = new(
        SimpleWithoutPartialWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SimpleWithoutPartialExtensions.WithAge");

    public static readonly Generated SimpleWithoutPartialWithAttributesExtensions = new(
        SimpleWithoutPartialWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SimpleWithoutPartialExtensions.WithAttributes");

    public static readonly Generated SimpleWithoutPartialWithNameExtensions = new(
        SimpleWithoutPartialWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SimpleWithoutPartialExtensions.WithName");

    private const string SimpleWithoutPartialWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class SimpleWithoutPartialExtensions
            {
                public static global::Fluentify.Records.Testing.SimpleWithoutPartial WithAge(this global::Fluentify.Records.Testing.SimpleWithoutPartial subject, int value)
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

    private const string SimpleWithoutPartialWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class SimpleWithoutPartialExtensions
            {
                public static global::Fluentify.Records.Testing.SimpleWithoutPartial WithAttributes(this global::Fluentify.Records.Testing.SimpleWithoutPartial subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Records.Testing.SimpleWithoutPartial WithAttributes(this global::Fluentify.Records.Testing.SimpleWithoutPartial subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.SimpleWithoutPartial WithAttributes(this global::Fluentify.Records.Testing.SimpleWithoutPartial subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.SimpleWithoutPartial WithAttributes(this global::Fluentify.Records.Testing.SimpleWithoutPartial subject, params object[] values)
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

    private const string SimpleWithoutPartialWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class SimpleWithoutPartialExtensions
            {
                public static global::Fluentify.Records.Testing.SimpleWithoutPartial WithName(this global::Fluentify.Records.Testing.SimpleWithoutPartial subject, string value)
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