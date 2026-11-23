namespace Fluentify.Snippets;

public static partial class Records
{
    public const string CrossReferencedContent = """
        namespace Fluentify.Records.Testing
        {
            [Fluentify]
            public sealed partial record CrossReferenced(string Description, Simple Simple);
        }
        """;

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

    private const string CrossReferencedConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            partial record CrossReferenced
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [global::System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
                #endif
                public CrossReferenced()
                    : this(default(string), default(global::Fluentify.Records.Testing.Simple))
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
            public static partial class CrossReferencedExtensions
            {
                public static global::Fluentify.Records.Testing.CrossReferenced WithDescription(this global::Fluentify.Records.Testing.CrossReferenced subject, string value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

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
            public static partial class CrossReferencedExtensions
            {
                public static global::Fluentify.Records.Testing.CrossReferenced WithSimple(this global::Fluentify.Records.Testing.CrossReferenced subject, global::Fluentify.Records.Testing.Simple instance, global::System.Func<global::Fluentify.Records.Testing.Simple, global::Fluentify.Records.Testing.Simple> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithSimple(instance);
                }

                public static global::Fluentify.Records.Testing.CrossReferenced WithSimple(this global::Fluentify.Records.Testing.CrossReferenced subject, global::System.Func<global::Fluentify.Records.Testing.Simple, global::Fluentify.Records.Testing.Simple> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = subject.Simple;

                    if (global::System.Object.ReferenceEquals(instance, null))
                    {
                        instance = new global::Fluentify.Records.Testing.Simple();
                    }

                    instance = builder(instance);

                    return subject.WithSimple(instance);
                }

                public static global::Fluentify.Records.Testing.CrossReferenced WithSimple(this global::Fluentify.Records.Testing.CrossReferenced subject, global::Fluentify.Records.Testing.Simple value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

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