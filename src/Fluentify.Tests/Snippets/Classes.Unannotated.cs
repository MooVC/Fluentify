namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string UnannotatedContent = """
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

    public static readonly Declared Unannotated;
}