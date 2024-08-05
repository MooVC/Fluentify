namespace Fluentify.Snippets;

public static partial class Classes
{
    public static readonly Declared Unsupported;

    private const string UnsupportedContent = """
        namespace Fluentify.Classes.Testing
        {
            [Fluentify]
            public sealed class Unsupported
            {
            }
        }
        """;
}