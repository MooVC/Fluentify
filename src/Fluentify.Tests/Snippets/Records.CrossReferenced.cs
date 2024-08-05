namespace Fluentify.Snippets;

public static partial class Records
{
    public static readonly Declared CrossReferenced;

    public static readonly Generated CrossReferencedConstructor = new(
        CrossReferencedConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.CrossReferenced.ctor");

    public static readonly Generated CrossReferencedWithDescriptionExtensions = new(
        CrossReferencedWithDescriptionExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.CrossReferencedExtensions.WithDescription");

    public static readonly Generated CrossReferencedWithSimpleExtensions = new(
        CrossReferencedWithSimpleExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.CrossReferencedExtensions.WithSimple");

    private const string CrossReferencedContent = """
        namespace Fluentify.Records.Testing
        {
            [Fluentify]
            public sealed partial record CrossReferenced(string Description, Simple Simple);
        }
        """;

    private const string CrossReferencedConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial record CrossReferenced
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public CrossReferenced()
                    : this(default, default)
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string CrossReferencedWithDescriptionExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class CrossReferencedExtensions
            {
                public static global::Fluentify.Records.Testing.CrossReferenced WithDescription(
                    this global::Fluentify.Records.Testing.CrossReferenced subject,
                    string value)
                {
                    subject.ThrowIfNull("subject");

                    return subject with
                    {
                        Description = value,
                    };
                }
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string CrossReferencedWithSimpleExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class CrossReferencedExtensions
            {
                public static global::Fluentify.Records.Testing.CrossReferenced WithSimple(
                    this global::Fluentify.Records.Testing.CrossReferenced subject,
                    Func<global::Fluentify.Records.Testing.Simple, global::Fluentify.Records.Testing.Simple> builder)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    var instance = new global::Fluentify.Records.Testing.Simple();

                    instance = builder(instance);

                    return subject.WithSimple(instance);
                }

                public static global::Fluentify.Records.Testing.CrossReferenced WithSimple(
                    this global::Fluentify.Records.Testing.CrossReferenced subject,
                    global::Fluentify.Records.Testing.Simple value)
                {
                    subject.ThrowIfNull("subject");

                    return subject with
                    {
                        Simple = value,
                    };
                }
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;
}