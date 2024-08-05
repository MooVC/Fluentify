namespace Fluentify.Snippets;

public static partial class Records
{
    public static readonly Declared Unsupported;

    private const string UnsupportedContent = """
        namespace Fluentify.Records.Testing
        {
            [Fluentify]
            public sealed record Unsupported();
        }
        """;
}