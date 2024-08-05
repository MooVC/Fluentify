namespace Fluentify.Snippets;

public static partial class Classes
{
    public static readonly Declared CrossReferenced;

    public static readonly Generated CrossReferencedWithDescriptionExtensions = new(
        CrossReferencedWithDescriptionExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.CrossReferencedExtensions.WithDescription");

    public static readonly Generated CrossReferencedWithSimpleExtensions = new(
        CrossReferencedWithSimpleExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.CrossReferencedExtensions.WithSimple");

    private const string CrossReferencedContent = """
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

    private const string CrossReferencedWithDescriptionExtensionsContent = """
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif
        
        #pragma warning disable CS8625

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

                    return new global::Fluentify.Classes.Testing.CrossReferenced
                    {
                        Description = value,
                        Simple = subject.Simple,
                    };
                }
            }
        }

        #pragma warning restore CS8625
        
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;

    private const string CrossReferencedWithSimpleExtensionsContent = """
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif
        
        #pragma warning disable CS8625

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
                    Func<global::Fluentify.Classes.Testing.Simple, global::Fluentify.Classes.Testing.Simple> builder)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    var instance = new global::Fluentify.Classes.Testing.Simple();

                    instance = builder(instance);

                    return subject.WithSimple(instance);
                }

                public static global::Fluentify.Classes.Testing.CrossReferenced WithSimple(
                    this global::Fluentify.Classes.Testing.CrossReferenced subject,
                    global::Fluentify.Classes.Testing.Simple value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.CrossReferenced
                    {
                        Description = subject.Description,
                        Simple = value,
                    };
                }
            }
        }

        #pragma warning restore CS8625
        
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;
}