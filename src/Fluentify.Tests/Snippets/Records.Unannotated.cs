namespace Fluentify.Snippets;

public static partial class Records
{
    public const string UnannotatedContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            public sealed partial record Unannotated(int Age, string Name, IReadOnlyList<object>? Attributes = default);
        }
        """;

    public static readonly Declared Unannotated;
}