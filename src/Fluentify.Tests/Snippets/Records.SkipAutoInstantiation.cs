namespace Fluentify.Snippets;

public static partial class Records
{
    public const string SkipAutoInstantiationContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record SkipAutoInstantiation(
                int Age,
                [SkipAutoInstantiation] SkipAutoInstantiation.Dependent Dependency)
            {
                public sealed class Dependent
                {
                    public string Name { get; set; }
                }
            }
        }
        """;

    public static readonly Declared SkipAutoInstantiation;

    public static readonly Generated SkipAutoInstantiationConstructor = new(
        SkipAutoInstantiationConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SkipAutoInstantiation.ctor");

    public static readonly Generated SkipAutoInstantiationWithAgeExtensions = new(
        SkipAutoInstantiationWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SkipAutoInstantiationExtensions.WithAge");

    public static readonly Generated SkipAutoInstantiationWithDependencyExtensions = new(
        SkipAutoInstantiationWithDependencyExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SkipAutoInstantiationExtensions.WithDependency");

    private const string SkipAutoInstantiationConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System.Diagnostics.CodeAnalysis;

            partial record SkipAutoInstantiation
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [SetsRequiredMembers]
                #endif
                public SkipAutoInstantiation()
                    : this(default(int), default(global::Fluentify.Records.Testing.SkipAutoInstantiation.Dependent))
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string SkipAutoInstantiationWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SkipAutoInstantiationExtensions
            {
                public static global::Fluentify.Records.Testing.SkipAutoInstantiation WithAge(
                    this global::Fluentify.Records.Testing.SkipAutoInstantiation subject,
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

    private const string SkipAutoInstantiationWithDependencyExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SkipAutoInstantiationExtensions
            {
                public static global::Fluentify.Records.Testing.SkipAutoInstantiation WithDependency(
                    this global::Fluentify.Records.Testing.SkipAutoInstantiation subject,
                    global::Fluentify.Records.Testing.SkipAutoInstantiation.Dependent value)
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