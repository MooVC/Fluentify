namespace Fluentify.Snippets;

public static partial class Records
{
    public const string SkipAutoInstantiationOnPropertyContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record SkipAutoInstantiationOnProperty(
                int Age,
                [SkipAutoInstantiation] SkipAutoInstantiationOnProperty.Dependent Dependency)
            {
                public sealed class Dependent
                {
                    public string Name { get; set; }
                }
            }
        }
        """;

    public static readonly Declared SkipAutoInstantiationOnProperty;

    public static readonly Generated SkipAutoInstantiationOnPropertyConstructor = new(
        SkipAutoInstantiationOnPropertyConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SkipAutoInstantiationOnProperty.ctor");

    public static readonly Generated SkipAutoInstantiationOnPropertyWithAgeExtensions = new(
        SkipAutoInstantiationOnPropertyWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SkipAutoInstantiationOnPropertyExtensions.WithAge");

    public static readonly Generated SkipAutoInstantiationOnPropertyWithDependencyExtensions = new(
        SkipAutoInstantiationOnPropertyWithDependencyExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SkipAutoInstantiationOnPropertyExtensions.WithDependency");

    private const string SkipAutoInstantiationOnPropertyConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial record SkipAutoInstantiationOnProperty
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public SkipAutoInstantiationOnProperty()
                    : this(default(int), default(global::Fluentify.Records.Testing.SkipAutoInstantiationOnProperty.Dependent))
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string SkipAutoInstantiationOnPropertyWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SkipAutoInstantiationOnPropertyExtensions
            {
                public static global::Fluentify.Records.Testing.SkipAutoInstantiationOnProperty WithAge(
                    this global::Fluentify.Records.Testing.SkipAutoInstantiationOnProperty subject,
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

    private const string SkipAutoInstantiationOnPropertyWithDependencyExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SkipAutoInstantiationOnPropertyExtensions
            {
                public static global::Fluentify.Records.Testing.SkipAutoInstantiationOnProperty WithDependency(
                    this global::Fluentify.Records.Testing.SkipAutoInstantiationOnProperty subject,
                    Func<global::Fluentify.Records.Testing.SkipAutoInstantiationOnProperty.Dependent, global::Fluentify.Records.Testing.SkipAutoInstantiationOnProperty.Dependent> builder)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    var instance = subject.Dependency;

                    if (instance != null)
                    {
                        throw new NotSupportedException();
                    }

                    instance = builder(instance);

                    return subject.WithDependency(instance);
                }

                public static global::Fluentify.Records.Testing.SkipAutoInstantiationOnProperty WithDependency(
                    this global::Fluentify.Records.Testing.SkipAutoInstantiationOnProperty subject,
                    global::Fluentify.Records.Testing.SkipAutoInstantiationOnProperty.Dependent value)
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