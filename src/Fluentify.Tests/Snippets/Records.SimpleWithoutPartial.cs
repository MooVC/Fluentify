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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SimpleWithoutPartialExtensions
            {
                public static global::Fluentify.Records.Testing.SimpleWithoutPartial WithAge(
                    this global::Fluentify.Records.Testing.SimpleWithoutPartial subject,
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

    private const string SimpleWithoutPartialWithAttributesExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SimpleWithoutPartialExtensions
            {
                public static global::Fluentify.Records.Testing.SimpleWithoutPartial WithAttributes(
                    this global::Fluentify.Records.Testing.SimpleWithoutPartial subject,
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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SimpleWithoutPartialExtensions
            {
                public static global::Fluentify.Records.Testing.SimpleWithoutPartial WithName(
                    this global::Fluentify.Records.Testing.SimpleWithoutPartial subject,
                    string value)
                {
                    subject.ThrowIfNull("subject");

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