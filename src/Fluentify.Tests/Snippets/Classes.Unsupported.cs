namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string UnsupportedContent = """
        namespace Fluentify.Classes.Testing
        {
            [Fluentify]
            public sealed class Unsupported
            {
            }
        }
        """;

    public static readonly Declared Unsupported;
}