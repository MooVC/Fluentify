namespace Fluentify.Snippets;

using System.Diagnostics.CodeAnalysis;

public static partial class Records
{
    public const string SingleContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record Single(int Age);
        }
        """;

    [SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "The name is appropriate.")]
    public static readonly Declared Single;

    public static readonly Generated SingleConstructor = new(
        SingleConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.Single.ctor");

    public static readonly Generated SingleWithAgeExtensions = new(
        SingleWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SingleExtensions.WithAge");

    private const string SingleConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial record Single
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public Single()
                    : this(default(int))
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string SingleWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SingleExtensions
            {
                public static global::Fluentify.Records.Testing.Single WithAge(
                    this global::Fluentify.Records.Testing.Single subject,
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
}