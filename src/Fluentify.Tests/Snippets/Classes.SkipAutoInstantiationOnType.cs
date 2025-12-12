namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string SkipAutoInstantiationOnTypeContent = """
        namespace Fluentify.Classes.Testing
        {
            [Fluentify]
            public sealed class SkipAutoInstantiationOnType
            {
                public int Age { get; set; }

                public Dependent Dependency { get; set; }

                [SkipAutoInstantiation]
                public sealed class Dependent
                {
                    public string Name { get; set; }
                }
            }
        }
        """;

    public static readonly Declared SkipAutoInstantiationOnType;

    public static readonly Generated SkipAutoInstantiationOnTypeWithAgeExtensions = new(
        SkipAutoInstantiationOnTypeWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SkipAutoInstantiationOnTypeExtensions.WithAge");

    public static readonly Generated SkipAutoInstantiationOnTypeWithDependencyExtensions = new(
        SkipAutoInstantiationOnTypeWithDependencyExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SkipAutoInstantiationOnTypeExtensions.WithDependency");

    private const string SkipAutoInstantiationOnTypeWithAgeExtensionsContent = """
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

            public static partial class SkipAutoInstantiationOnTypeExtensions
            {
                public static global::Fluentify.Classes.Testing.SkipAutoInstantiationOnType WithAge(
                    this global::Fluentify.Classes.Testing.SkipAutoInstantiationOnType subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SkipAutoInstantiationOnType
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

    private const string SkipAutoInstantiationOnTypeWithDependencyExtensionsContent = """
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

            public static partial class SkipAutoInstantiationOnTypeExtensions
            {
                public static global::Fluentify.Classes.Testing.SkipAutoInstantiationOnType WithDependency(
                    this global::Fluentify.Classes.Testing.SkipAutoInstantiationOnType subject,
                    Func<global::Fluentify.Classes.Testing.SkipAutoInstantiationOnType.Dependent, global::Fluentify.Classes.Testing.SkipAutoInstantiationOnType.Dependent> builder)
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

                public static global::Fluentify.Classes.Testing.SkipAutoInstantiationOnType WithDependency(
                    this global::Fluentify.Classes.Testing.SkipAutoInstantiationOnType subject,
                    global::Fluentify.Classes.Testing.SkipAutoInstantiationOnType.Dependent value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SkipAutoInstantiationOnType
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