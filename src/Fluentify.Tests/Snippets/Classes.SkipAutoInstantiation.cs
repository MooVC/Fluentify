namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string SkipAutoInstantiationContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class SkipAutoInstantiation
            {
                public int Age { get; set; }

                [SkipAutoInstantiation]
                public Dependent Dependency { get; set; }

                public sealed class Dependent
                {
                    public string Name { get; set; }
                }
            }
        }
        """;

    public static readonly Declared SkipAutoInstantiation;

    public static readonly Generated SkipAutoInstantiationWithAgeExtensions = new(
        SkipAutoInstantiationWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SkipAutoInstantiationExtensions.WithAge");

    public static readonly Generated SkipAutoInstantiationWithDependencyExtensions = new(
        SkipAutoInstantiationWithDependencyExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SkipAutoInstantiationExtensions.WithDependency");

    private const string SkipAutoInstantiationWithAgeExtensionsContent = """
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

            public static partial class SkipAutoInstantiationExtensions
            {
                public static global::Fluentify.Classes.Testing.SkipAutoInstantiation WithAge(
                    this global::Fluentify.Classes.Testing.SkipAutoInstantiation subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SkipAutoInstantiation
                    {
                        Age = value,
                        Dependency = subject.Dependency,
                    };
                }
            }
        }

        #pragma warning restore CS8625

        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;

    private const string SkipAutoInstantiationWithDependencyExtensionsContent = """
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

            public static partial class SkipAutoInstantiationExtensions
            {
                public static global::Fluentify.Classes.Testing.SkipAutoInstantiation WithDependency(
                    this global::Fluentify.Classes.Testing.SkipAutoInstantiation subject,
                    global::Fluentify.Classes.Testing.SkipAutoInstantiation.Dependent value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SkipAutoInstantiation
                    {
                        Age = subject.Age,
                        Dependency = value,
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