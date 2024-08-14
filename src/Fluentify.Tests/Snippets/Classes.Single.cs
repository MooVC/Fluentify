namespace Fluentify.Snippets;

using System.Diagnostics.CodeAnalysis;

public static partial class Classes
{
    public const string SingleContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class Single
            {
                public int Age { get; set; }
            }
        }
        """;

    [SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "The name is appropriate.")]
    public static readonly Declared Single;

    public static readonly Generated SingleWithAgeExtensions = new(
        SingleWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SingleExtensions.WithAge");

    private const string SingleWithAgeExtensionsContent = """
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

            public static partial class SingleExtensions
            {
                public static global::Fluentify.Classes.Testing.Single WithAge(
                    this global::Fluentify.Classes.Testing.Single subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.Single
                    {
                        Age = value,
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