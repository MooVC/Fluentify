namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string CrossReferencedContent = """
        namespace Fluentify.Classes.Testing
        {
            [Fluentify]
            public sealed class CrossReferenced
            {
                public string Description { get; set; }

                public Simple Simple { get; set; }
            }
        }
        """;

    public static readonly Declared CrossReferenced;

    public static readonly Generated CrossReferencedWithDescriptionExtensions = new(
        CrossReferencedWithDescriptionExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.CrossReferencedExtensions.WithDescription");

    public static readonly Generated CrossReferencedWithSimpleExtensions = new(
        CrossReferencedWithSimpleExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.CrossReferencedExtensions.WithSimple");

    public static readonly Generated CrossReferencedWithExtensions = new(
        CrossReferencedWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.CrossReferencedExtensions.With");

    private const string CrossReferencedWithDescriptionExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class CrossReferencedExtensions
            {
                public static global::Fluentify.Classes.Testing.CrossReferenced WithDescription(this global::Fluentify.Classes.Testing.CrossReferenced subject, string value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.CrossReferenced
                    {
                        Description = value,
                        Simple = subject.Simple,
                    };
                }
            }
        }
        """;

    private const string CrossReferencedWithSimpleExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class CrossReferencedExtensions
            {
                public static global::Fluentify.Classes.Testing.CrossReferenced WithSimple(this global::Fluentify.Classes.Testing.CrossReferenced subject, global::Fluentify.Classes.Testing.Simple instance, global::System.Func<global::Fluentify.Classes.Testing.Simple, global::Fluentify.Classes.Testing.Simple> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithSimple(instance);
                }

                public static global::Fluentify.Classes.Testing.CrossReferenced WithSimple(this global::Fluentify.Classes.Testing.CrossReferenced subject, global::System.Func<global::Fluentify.Classes.Testing.Simple, global::Fluentify.Classes.Testing.Simple> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = subject.Simple;

                    if (global::System.Object.ReferenceEquals(instance, null))
                    {
                        instance = new global::Fluentify.Classes.Testing.Simple();
                    }

                    instance = builder(instance);

                    return subject.WithSimple(instance);
                }

                public static global::Fluentify.Classes.Testing.CrossReferenced WithSimple(this global::Fluentify.Classes.Testing.CrossReferenced subject, global::Fluentify.Classes.Testing.Simple value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.CrossReferenced
                    {
                        Description = subject.Description,
                        Simple = value,
                    };
                }
            }
        }
        """;

    private const string CrossReferencedWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class CrossReferencedExtensions
            {
                internal static global::Fluentify.Classes.Testing.CrossReferenced With(this global::Fluentify.Classes.Testing.CrossReferenced subject, global::System.Func<string> description = default, global::System.Func<global::Fluentify.Classes.Testing.Simple> simple = default)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    var descriptionValue = global::System.Object.ReferenceEquals(description, null) ? subject.Description : description();
                    var simpleValue = global::System.Object.ReferenceEquals(simple, null) ? subject.Simple : simple();

                    global::Fluentify.Internal.Extensions.ThrowIfNull(descriptionValue, "description");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(simpleValue, "simple");

                    return new global::Fluentify.Classes.Testing.CrossReferenced
                    {
                        Description = descriptionValue,
                        Simple = simpleValue,
                    };
                }
            }
        }
        """;
}