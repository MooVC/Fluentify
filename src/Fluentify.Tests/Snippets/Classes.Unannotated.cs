namespace Fluentify.Snippets;

public static partial class Classes
{
    public static readonly Declared Unannotated;

    private const string UnannotatedContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            public sealed class Unannotated
            {
                public int Age { get; set; }

                public string Name { get; set; }

                public IReadOnlyList<object> Attributes { get; set; }
            }
        }
        """;
}