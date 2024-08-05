namespace Fluentify.Snippets;

public static partial class Classes
{
    public static readonly Declared AllThreeIgnored;

    private const string AllThreeIgnoredContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class AllThreeIgnored
            {
                [Ignore]
                public int Age { get; set; }

                [Ignore]
                public string Name { get; set; }

                [Ignore]
                public IReadOnlyList<object> Attributes { get; set; }
            }
        }
        """;
}