namespace Fluentify.Snippets;

public static partial class Records
{
    public const string TwoOfThreeIgnoredContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record TwoOfThreeIgnored([Ignore] int Age, [Ignore] string Name, IReadOnlyList<object>? Attributes = default);
        }
        """;

    public static readonly Declared TwoOfThreeIgnored;

    public static readonly Generated TwoOfThreeIgnoredConstructor = new(
        TwoOfThreeIgnoredConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.TwoOfThreeIgnored.ctor");

    public static readonly Generated TwoOfThreeIgnoredWithAttributesExtensions = new(
        TwoOfThreeIgnoredWithAttributesExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.TwoOfThreeIgnoredExtensions.WithAttributes");

    private const string TwoOfThreeIgnoredConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial record TwoOfThreeIgnored
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public TwoOfThreeIgnored()
                    : this(default(int), default(string), default(global::System.Collections.Generic.IReadOnlyList<object>?))
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string TwoOfThreeIgnoredWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class TwoOfThreeIgnoredExtensions
            {
                public static global::Fluentify.Records.Testing.TwoOfThreeIgnored WithAttributes(
                    this global::Fluentify.Records.Testing.TwoOfThreeIgnored subject,
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

                public static global::Fluentify.Records.Testing.TwoOfThreeIgnored WithAttributes(
                    this global::Fluentify.Records.Testing.TwoOfThreeIgnored subject,
                    Func<object, object> builder)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;
}