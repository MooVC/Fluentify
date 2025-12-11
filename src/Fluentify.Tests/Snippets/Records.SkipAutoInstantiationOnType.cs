namespace Fluentify.Snippets;

public static partial class Records
{
    public const string SkipAutoInstantiationOnTypeContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record SkipAutoInstantiationOnType(
                int Age,
                SkipAutoInstantiationOnType.Dependent Dependency)
            {
                [SkipAutoInstantiation]
                public sealed class Dependent
                {
                    public string Name { get; set; }
                }
            }
        }
        """;

    public static readonly Declared SkipAutoInstantiationOnType;

    public static readonly Generated SkipAutoInstantiationOnTypeConstructor = new(
        SkipAutoInstantiationOnTypeConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SkipAutoInstantiationOnType.ctor");

    public static readonly Generated SkipAutoInstantiationOnTypeWithAgeExtensions = new(
        SkipAutoInstantiationOnTypeWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SkipAutoInstantiationOnTypeExtensions.WithAge");

    public static readonly Generated SkipAutoInstantiationOnTypeWithDependencyExtensions = new(
        SkipAutoInstantiationOnTypeWithDependencyExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SkipAutoInstantiationOnTypeExtensions.WithDependency");

    private const string SkipAutoInstantiationOnTypeConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial record SkipAutoInstantiationOnType
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public SkipAutoInstantiationOnType()
                    : this(default(int), default(global::Fluentify.Records.Testing.SkipAutoInstantiationOnType.Dependent))
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string SkipAutoInstantiationOnTypeWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SkipAutoInstantiationOnTypeExtensions
            {
                public static global::Fluentify.Records.Testing.SkipAutoInstantiationOnType WithAge(
                    this global::Fluentify.Records.Testing.SkipAutoInstantiationOnType subject,
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

    private const string SkipAutoInstantiationOnTypeWithDependencyExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SkipAutoInstantiationOnTypeExtensions
            {
                public static global::Fluentify.Records.Testing.SkipAutoInstantiationOnType WithDependency(
                    this global::Fluentify.Records.Testing.SkipAutoInstantiationOnType subject,
                    Func<global::Fluentify.Records.Testing.SkipAutoInstantiationOnType.Dependent, global::Fluentify.Records.Testing.SkipAutoInstantiationOnType.Dependent> builder)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    var instance = subject.Dependency;

                    if (instance is null)
                    {
                        throw new NotSupportedException();
                    }

                    instance = builder(instance);

                    return subject.WithDependency(instance);
                }

                public static global::Fluentify.Records.Testing.SkipAutoInstantiationOnType WithDependency(
                    this global::Fluentify.Records.Testing.SkipAutoInstantiationOnType subject,
                    global::Fluentify.Records.Testing.SkipAutoInstantiationOnType.Dependent value)
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