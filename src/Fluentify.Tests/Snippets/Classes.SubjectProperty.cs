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
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif
        
        #pragma warning disable CS8625

        namespace Fluentify.Classes.Testing
        {
            using System;
            using Fluentify.Internal;

            public static partial class SubjectPropertyExtensions
            {
                internal static global::Fluentify.Classes.Testing.SubjectProperty With(
                    this global::Fluentify.Classes.Testing.SubjectProperty subject,
                    Func<string> subject1 = default)
                {
                    subject.ThrowIfNull("subject");

                    var subject1Value = ReferenceEquals(subject1, null) ? subject.Subject : subject1();

                    return new global::Fluentify.Classes.Testing.SubjectProperty
                    {
                        Subject = subject1Value,
                    };
                }
            }
        }

        #pragma warning restore CS8625
        
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;

    private const string SubjectPropertyWithSubjectExtensionsContent = """
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

            public static partial class SubjectPropertyExtensions
            {
                public static global::Fluentify.Classes.Testing.SubjectProperty WithSubject(
                    this global::Fluentify.Classes.Testing.SubjectProperty subject,
                    string value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SubjectProperty
                    {
                        Subject = value,
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