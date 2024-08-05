namespace Fluentify.Snippets;

public static partial class Records
{
    public const string UnsupportedContent = """
        namespace Fluentify.Records.Testing
        {
            [Fluentify]
            public sealed record Unsupported();
        }
        """;

    public static readonly Declared Unsupported;
}