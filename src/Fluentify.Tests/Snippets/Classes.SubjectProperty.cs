namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string SubjectPropertyContent = """
        namespace Fluentify.Classes.Testing
        {
            [Fluentify]
            public sealed class SubjectProperty
            {
                public string Subject { get; set; }
            }
        }
        """;

    public static readonly Declared SubjectProperty;

    public static readonly Generated SubjectPropertyWithExtensions = new(
        SubjectPropertyWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SubjectPropertyExtensions.With");

    public static readonly Generated SubjectPropertyWithSubjectExtensions = new(
        SubjectPropertyWithSubjectExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SubjectPropertyExtensions.WithSubject");

    private const string SubjectPropertyWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SubjectPropertyExtensions
            {
                internal static global::Fluentify.Classes.Testing.SubjectProperty With(this global::Fluentify.Classes.Testing.SubjectProperty subject, global::System.Func<string> subject1 = default)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    var subject1Value = global::System.Object.ReferenceEquals(subject1, null) ? subject.Subject : subject1();

                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject1Value, "subject1");

                    return new global::Fluentify.Classes.Testing.SubjectProperty
                    {
                        Subject = subject1Value,
                    };
                }
            }
        }
        """;

    private const string SubjectPropertyWithSubjectExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SubjectPropertyExtensions
            {
                public static global::Fluentify.Classes.Testing.SubjectProperty WithSubject(this global::Fluentify.Classes.Testing.SubjectProperty subject, string value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.SubjectProperty
                    {
                        Subject = value,
                    };
                }
            }
        }
        """;
}