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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class CrossReferencedExtensions
            {
                public static global::Fluentify.Classes.Testing.CrossReferenced WithDescription(
                    this global::Fluentify.Classes.Testing.CrossReferenced subject,
                    string value)
                {
                    subject.ThrowIfNull("subject");

                    value.ThrowIfNull("value");

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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class CrossReferencedExtensions
            {
                public static global::Fluentify.Classes.Testing.CrossReferenced WithSimple(
                    this global::Fluentify.Classes.Testing.CrossReferenced subject,
                    global::Fluentify.Classes.Testing.Simple instance,
                    Func<global::Fluentify.Classes.Testing.Simple, global::Fluentify.Classes.Testing.Simple> builder)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    instance = builder(instance);

                    return subject.WithSimple(instance);
                }

                public static global::Fluentify.Classes.Testing.CrossReferenced WithSimple(
                    this global::Fluentify.Classes.Testing.CrossReferenced subject,
                    Func<global::Fluentify.Classes.Testing.Simple, global::Fluentify.Classes.Testing.Simple> builder)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    var instance = subject.Simple;

                    if (ReferenceEquals(instance, null))
                    {
                        instance = new global::Fluentify.Classes.Testing.Simple();
                    }

                    instance = builder(instance);

                    return subject.WithSimple(instance);
                }

                public static global::Fluentify.Classes.Testing.CrossReferenced WithSimple(
                    this global::Fluentify.Classes.Testing.CrossReferenced subject,
                    global::Fluentify.Classes.Testing.Simple value)
                {
                    subject.ThrowIfNull("subject");

                    value.ThrowIfNull("value");

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
            using System;
            using Fluentify.Internal;

            public static partial class CrossReferencedExtensions
            {
                internal static global::Fluentify.Classes.Testing.CrossReferenced With(
                    this global::Fluentify.Classes.Testing.CrossReferenced subject,
                    Func<string> description = default,
                    Func<global::Fluentify.Classes.Testing.Simple> simple = default)
                {
                    subject.ThrowIfNull("subject");

                    var descriptionValue = ReferenceEquals(description, null) ? subject.Description : description();
                    var simpleValue = ReferenceEquals(simple, null) ? subject.Simple : simple();

                    descriptionValue.ThrowIfNull("description");
                    simpleValue.ThrowIfNull("simple");

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