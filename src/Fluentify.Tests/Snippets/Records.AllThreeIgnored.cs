namespace Fluentify.Snippets;

public static partial class Records
{
    public static readonly Declared AllThreeIgnored;

    public static readonly Generated AllThreeIgnoredConstructor = new(
        AllThreeIgnoredConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.AllThreeIgnored.ctor");

    private const string AllThreeIgnoredContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record AllThreeIgnored([Ignore] int Age, [Ignore] string Name, [Ignore] IReadOnlyList<object>? Attributes = default);
        }
        """;

    private const string AllThreeIgnoredConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial record AllThreeIgnored
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public AllThreeIgnored()
                    : this(default, default, default)
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;
}