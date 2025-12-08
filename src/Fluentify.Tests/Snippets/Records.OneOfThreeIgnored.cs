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
            using System.Diagnostics.CodeAnalysis;

            partial record OneOfThreeIgnored
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class OneOfThreeIgnoredExtensions
            {
                public static global::Fluentify.Records.Testing.OneOfThreeIgnored WithAge(
                    this global::Fluentify.Records.Testing.OneOfThreeIgnored subject,
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

    private const string OneOfThreeIgnoredWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class OneOfThreeIgnoredExtensions
            {
                public static global::Fluentify.Records.Testing.OneOfThreeIgnored WithAttributes(
                    this global::Fluentify.Records.Testing.OneOfThreeIgnored subject,
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

                public static global::Fluentify.Records.Testing.OneOfThreeIgnored WithAttributes(
                    this global::Fluentify.Records.Testing.OneOfThreeIgnored subject,
                    Func<object, object> builder)
                {
                    subject.ThrowIfNull("subject");
                    builder.ThrowIfNull("builder");

                    var instance = subject.Attributes?.FirstOrDefault();

                    if (instance is null)
                    {
                        instance = new object();
                    }

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;
}