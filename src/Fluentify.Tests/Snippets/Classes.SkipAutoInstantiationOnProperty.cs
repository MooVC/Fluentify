namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string SkipAutoInstantiationOnPropertyContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class SkipAutoInstantiationOnProperty
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

    public static readonly Declared SkipAutoInstantiationOnProperty;

    public static readonly Generated SkipAutoInstantiationOnPropertyWithAgeExtensions = new(
        SkipAutoInstantiationOnPropertyWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SkipAutoInstantiationOnPropertyExtensions.WithAge");

    public static readonly Generated SkipAutoInstantiationOnPropertyWithDependencyExtensions = new(
        SkipAutoInstantiationOnPropertyWithDependencyExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SkipAutoInstantiationOnPropertyExtensions.WithDependency");

    private const string SkipAutoInstantiationOnPropertyWithAgeExtensionsContent = """
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

            public static partial class SkipAutoInstantiationOnPropertyExtensions
            {
                public static global::Fluentify.Classes.Testing.SkipAutoInstantiationOnProperty WithAge(
                    this global::Fluentify.Classes.Testing.SkipAutoInstantiationOnProperty subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SkipAutoInstantiationOnProperty
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

    private const string SkipAutoInstantiationOnPropertyWithDependencyExtensionsContent = """
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

            public static partial class SkipAutoInstantiationOnPropertyExtensions
            {
                public static global::Fluentify.Classes.Testing.SkipAutoInstantiationOnProperty WithDependency(
                    this global::Fluentify.Classes.Testing.SkipAutoInstantiationOnProperty subject,
                    global::Fluentify.Classes.Testing.SkipAutoInstantiationOnProperty.Dependent value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SkipAutoInstantiationOnProperty
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