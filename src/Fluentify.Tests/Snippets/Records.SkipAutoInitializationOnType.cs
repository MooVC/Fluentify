namespace Fluentify.Snippets;

public static partial class Records
{
    public const string SkipAutoInitializationOnTypeContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record SkipAutoInitializationOnType(
                int Age,
                SkipAutoInitializationOnType.Dependent Dependency)
            {
                [SkipAutoInitialization]
                public sealed class Dependent
                {
                    public string Name { get; set; }
                }
            }
        }
        """;

    public static readonly Declared SkipAutoInitializationOnType;

    public static readonly Generated SkipAutoInitializationOnTypeConstructor = new(
        SkipAutoInitializationOnTypeConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SkipAutoInitializationOnType.ctor");

    public static readonly Generated SkipAutoInitializationOnTypeWithAgeExtensions = new(
        SkipAutoInitializationOnTypeWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SkipAutoInitializationOnTypeExtensions.WithAge");

    public static readonly Generated SkipAutoInitializationOnTypeWithDependencyExtensions = new(
        SkipAutoInitializationOnTypeWithDependencyExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SkipAutoInitializationOnTypeExtensions.WithDependency");

    private const string SkipAutoInitializationOnTypeConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial record SkipAutoInitializationOnType
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public SkipAutoInitializationOnType()
                    : this(default(int), default(global::Fluentify.Records.Testing.SkipAutoInitializationOnType.Dependent))
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string SkipAutoInitializationOnTypeWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SkipAutoInitializationOnTypeExtensions
            {
                public static global::Fluentify.Records.Testing.SkipAutoInitializationOnType WithAge(
                    this global::Fluentify.Records.Testing.SkipAutoInitializationOnType subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

                    return subject with
                    {
                        Age = value,
                    };
                }
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string SkipAutoInitializationOnTypeWithDependencyExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SkipAutoInitializationOnTypeExtensions
            {
                public static global::Fluentify.Records.Testing.SkipAutoInitializationOnType WithDependency(
                    this global::Fluentify.Records.Testing.SkipAutoInitializationOnType subject,
                    Func<global::Fluentify.Records.Testing.SkipAutoInitializationOnType.Dependent, global::Fluentify.Records.Testing.SkipAutoInitializationOnType.Dependent> builder)
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

                public static global::Fluentify.Records.Testing.SkipAutoInitializationOnType WithDependency(
                    this global::Fluentify.Records.Testing.SkipAutoInitializationOnType subject,
                    global::Fluentify.Records.Testing.SkipAutoInitializationOnType.Dependent value)
                {
                    subject.ThrowIfNull("subject");

                    return subject with
                    {
                        Dependency = value,
                    };
                }
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;
}