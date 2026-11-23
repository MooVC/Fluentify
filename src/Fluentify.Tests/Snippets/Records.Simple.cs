namespace Fluentify.Snippets;

public static partial class Records
{
    public const string SimpleContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record Simple(int Age, string Name, IReadOnlyList<object>? Attributes = default);
        }
        """;

    public static readonly Declared Simple;

    public static readonly Generated SimpleConstructor = new(
        SimpleConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Simple.ctor");

    public static readonly Generated SimpleWithAgeExtensions = new(
        SimpleWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SimpleExtensions.WithAge");

    public static readonly Generated SimpleWithAttributesExtensions = new(
        SimpleWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SimpleExtensions.WithAttributes");

    public static readonly Generated SimpleWithNameExtensions = new(
        SimpleWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SimpleExtensions.WithName");

    private const string SimpleConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            partial record Simple
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [global::System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
                #endif
                public Simple()
                    : this(default(int), default(string), default(global::System.Collections.Generic.IReadOnlyList<object>?))
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string SimpleWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class SimpleExtensions
            {
                public static global::Fluentify.Records.Testing.Simple WithAge(this global::Fluentify.Records.Testing.Simple subject, int value)
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

    private const string SimpleWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class SimpleExtensions
            {
                public static global::Fluentify.Records.Testing.Simple WithAttributes(this global::Fluentify.Records.Testing.Simple subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Records.Testing.Simple WithAttributes(this global::Fluentify.Records.Testing.Simple subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.Simple WithAttributes(this global::Fluentify.Records.Testing.Simple subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Records.Testing.Simple WithAttributes(this global::Fluentify.Records.Testing.Simple subject, params object[] values)
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

    private const string SimpleWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class SimpleExtensions
            {
                public static global::Fluentify.Records.Testing.Simple WithName(this global::Fluentify.Records.Testing.Simple subject, string value)
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