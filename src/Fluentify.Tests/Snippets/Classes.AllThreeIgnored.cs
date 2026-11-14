namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string AllThreeIgnoredContent = """
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

    public static readonly Declared AllThreeIgnored;

    public static readonly Generated AllThreeIgnoredWithExtensions = new(
        AllThreeIgnoredWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.AllThreeIgnoredExtensions.With");

    private const string AllThreeIgnoredWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class AllThreeIgnoredExtensions
            {
                internal static global::Fluentify.Classes.Testing.AllThreeIgnored With(this global::Fluentify.Classes.Testing.AllThreeIgnored subject, global::System.Func<int> age = default, global::System.Func<global::System.Collections.Generic.IReadOnlyList<object>> attributes = default, global::System.Func<string> name = default)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    var ageValue = global::System.Object.ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = global::System.Object.ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = global::System.Object.ReferenceEquals(name, null) ? subject.Name : name();

                    global::Fluentify.Internal.Extensions.ThrowIfNull(attributesValue, "attributes");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(nameValue, "name");

                    return new global::Fluentify.Classes.Testing.AllThreeIgnored
                    {
                        Age = ageValue,
                        Attributes = attributesValue,
                        Name = nameValue,
                    };
                }
            }
        }
        """;
}