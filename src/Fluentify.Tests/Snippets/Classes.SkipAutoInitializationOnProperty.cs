namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string SkipAutoInitializationOnPropertyContent = """
        namespace Fluentify.Classes.Testing
        {
            [Fluentify]
            public sealed class SkipAutoInitializationOnProperty
            {
                public int Age { get; set; }

                [SkipAutoInitialization]
                public Dependent Dependency { get; set; }

                public sealed class Dependent
                {
                    public string Name { get; set; }
                }
            }
        }
        """;

    public static readonly Declared SkipAutoInitializationOnProperty;

    public static readonly Generated SkipAutoInitializationOnPropertyWithAgeExtensions = new(
        SkipAutoInitializationOnPropertyWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SkipAutoInitializationOnPropertyExtensions.WithAge");

    public static readonly Generated SkipAutoInitializationOnPropertyWithDependencyExtensions = new(
        SkipAutoInitializationOnPropertyWithDependencyExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SkipAutoInitializationOnPropertyExtensions.WithDependency");

    public static readonly Generated SkipAutoInitializationOnPropertyWithExtensions = new(
        SkipAutoInitializationOnPropertyWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SkipAutoInitializationOnPropertyExtensions.With");

    private const string SkipAutoInitializationOnPropertyWithAgeExtensionsContent = """
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

            public static partial class SkipAutoInitializationOnPropertyExtensions
            {
                public static global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty WithAge(
                    this global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty
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

    private const string SkipAutoInitializationOnPropertyWithDependencyExtensionsContent = """
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

            public static partial class SkipAutoInitializationOnPropertyExtensions
            {
                public static global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty WithDependency(
                    this global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty subject,
                    Func<global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty.Dependent, global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty.Dependent> builder)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    var instance = subject.Dependency;

                    if (ReferenceEquals(instance, null))
                    {
                        throw new NotSupportedException();
                    }

                    instance = builder(instance);

                    return subject.WithDependency(instance);
                }

                public static global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty WithDependency(
                    this global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty subject,
                    global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty.Dependent value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty
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

    private const string SkipAutoInitializationOnPropertyWithExtensionsContent = """
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif

        #pragma warning disable CS8625

        namespace Fluentify.Classes.Testing
        {
            using System;
            using Fluentify.Internal;

            public static partial class SkipAutoInitializationOnPropertyExtensions
            {
                internal static global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty With(
                    this global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty subject,
                    Func<int> age = default,
                    Func<global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty.Dependent> dependency = default)
                {
                    subject.ThrowIfNull("subject");

                    var ageValue = ReferenceEquals(age, null) ? subject.Age : age();
                    var dependencyValue = ReferenceEquals(dependency, null) ? subject.Dependency : dependency();

                    return new global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty
                    {
                        Age = ageValue,
                        Dependency = dependencyValue,
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