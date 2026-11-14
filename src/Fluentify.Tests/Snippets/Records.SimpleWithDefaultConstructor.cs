namespace Fluentify.Snippets;

public static partial class Records
{
    public const string SimpleWithDefaultConstructorContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;
            using System.Diagnostics.CodeAnalysis;

            [Fluentify]
            public sealed partial record SimpleWithDefaultConstructor(int Age, string Name, IReadOnlyList<object>? Attributes = default)
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public SimpleWithDefaultConstructor()
                    : this(default(int), default(string), default(global::System.Collections.Generic.IReadOnlyList<object>?))
                {
                }

                #pragma warning restore CS8604
            }
        }
        """;

    public static readonly Declared SimpleWithDefaultConstructor;

    public static readonly Generated SimpleWithDefaultConstructorWithAgeExtensions = new(
        SimpleWithDefaultConstructorWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SimpleWithDefaultConstructorExtensions.WithAge");

    public static readonly Generated SimpleWithDefaultConstructorWithAttributesExtensions = new(
        SimpleWithDefaultConstructorWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SimpleWithDefaultConstructorExtensions.WithAttributes");

    public static readonly Generated SimpleWithDefaultConstructorWithNameExtensions = new(
        SimpleWithDefaultConstructorWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SimpleWithDefaultConstructorExtensions.WithName");

    private const string SimpleWithDefaultConstructorWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class SimpleWithDefaultConstructorExtensions
            {
                public static global::Fluentify.Records.Testing.SimpleWithDefaultConstructor WithAge(this global::Fluentify.Records.Testing.SimpleWithDefaultConstructor subject, int value)
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

    private const string SimpleWithDefaultConstructorWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class SimpleWithDefaultConstructorExtensions
            {
                public static global::Fluentify.Records.Testing.SimpleWithDefaultConstructor WithAttributes(this global::Fluentify.Records.Testing.SimpleWithDefaultConstructor subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Records.Testing.SimpleWithDefaultConstructor WithAttributes(this global::Fluentify.Records.Testing.SimpleWithDefaultConstructor subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.SimpleWithDefaultConstructor WithAttributes(this global::Fluentify.Records.Testing.SimpleWithDefaultConstructor subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.SimpleWithDefaultConstructor WithAttributes(this global::Fluentify.Records.Testing.SimpleWithDefaultConstructor subject, params object[] values)
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

    private const string SimpleWithDefaultConstructorWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class SimpleWithDefaultConstructorExtensions
            {
                public static global::Fluentify.Records.Testing.SimpleWithDefaultConstructor WithName(this global::Fluentify.Records.Testing.SimpleWithDefaultConstructor subject, string value)
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