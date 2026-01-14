namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string SkipAutoInitializationOnTypeContent = """
        namespace Fluentify.Classes.Testing
        {
            [Fluentify]
            public sealed class SkipAutoInitializationOnType
            {
                public int Age { get; set; }

                public Dependent Dependency { get; set; }

                [SkipAutoInitialization]
                public sealed class Dependent
                {
                    public string Name { get; set; }
                }
            }
        }
        """;

    public static readonly Declared SkipAutoInitializationOnType;

    public static readonly Generated SkipAutoInitializationOnTypeWithAgeExtensions = new(
        SkipAutoInitializationOnTypeWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SkipAutoInitializationOnTypeExtensions.WithAge");

    public static readonly Generated SkipAutoInitializationOnTypeWithDependencyExtensions = new(
        SkipAutoInitializationOnTypeWithDependencyExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SkipAutoInitializationOnTypeExtensions.WithDependency");

    private const string SkipAutoInitializationOnTypeWithAgeExtensionsContent = """
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

            public static partial class SkipAutoInitializationOnTypeExtensions
            {
                public static global::Fluentify.Classes.Testing.SkipAutoInitializationOnType WithAge(
                    this global::Fluentify.Classes.Testing.SkipAutoInitializationOnType subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SkipAutoInitializationOnType
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

    private const string SkipAutoInitializationOnTypeWithDependencyExtensionsContent = """
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

            public static partial class SkipAutoInitializationOnTypeExtensions
            {
                public static global::Fluentify.Classes.Testing.SkipAutoInitializationOnType WithDependency(
                    this global::Fluentify.Classes.Testing.SkipAutoInitializationOnType subject,
                    Func<global::Fluentify.Classes.Testing.SkipAutoInitializationOnType.Dependent, global::Fluentify.Classes.Testing.SkipAutoInitializationOnType.Dependent> builder)
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

                public static global::Fluentify.Classes.Testing.SkipAutoInitializationOnType WithDependency(
                    this global::Fluentify.Classes.Testing.SkipAutoInitializationOnType subject,
                    global::Fluentify.Classes.Testing.SkipAutoInitializationOnType.Dependent value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SkipAutoInitializationOnType
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