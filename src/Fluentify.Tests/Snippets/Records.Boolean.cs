namespace Fluentify.Snippets;

public static partial class Records
{
    public const string BooleanContent = """
        namespace Fluentify.Records.Testing
        {
            [Fluentify]
            public sealed partial record Boolean(int Age, bool IsRetired, string Name);
        }
        """;

    public static readonly Declared Boolean;

    public static readonly Generated BooleanConstructor = new(
        BooleanConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Boolean.ctor");

    public static readonly Generated BooleanWithAgeExtensions = new(
        BooleanWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.BooleanExtensions.WithAge");

    public static readonly Generated BooleanWithNameExtensions = new(
        BooleanWithNameExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.BooleanExtensions.WithName");

    public static readonly Generated BooleanIsRetiredExtensions = new(
        BooleanIsRetiredExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.BooleanExtensions.IsRetired");

    private const string BooleanConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial record Boolean
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public Boolean()
                    : this(default, default, default)
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string BooleanWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class BooleanExtensions
            {
                public static global::Fluentify.Records.Testing.Boolean WithAge(
                    this global::Fluentify.Records.Testing.Boolean subject,
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

    private const string BooleanWithNameExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class BooleanExtensions
            {
                public static global::Fluentify.Records.Testing.Boolean WithName(
                    this global::Fluentify.Records.Testing.Boolean subject,
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

    private const string BooleanIsRetiredExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class BooleanExtensions
            {
                public static global::Fluentify.Records.Testing.Boolean IsRetired(
                    this global::Fluentify.Records.Testing.Boolean subject,
                    bool value)
                {
                    subject.ThrowIfNull("subject");

                    return subject with
                    {
                        IsRetired = value,
                    };
                }
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;
}