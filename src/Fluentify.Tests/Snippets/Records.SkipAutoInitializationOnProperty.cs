namespace Fluentify.Snippets;

public static partial class Records
{
    public const string SkipAutoInitializationOnPropertyContent = """
        namespace Fluentify.Records.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed partial record SkipAutoInitializationOnProperty(
                int Age,
                [SkipAutoInitialization] SkipAutoInitializationOnProperty.Dependent Dependency)
            {
                public sealed class Dependent
                {
                    public string Name { get; set; }
                }
            }
        }
        """;

    public static readonly Declared SkipAutoInitializationOnProperty;

    public static readonly Generated SkipAutoInitializationOnPropertyConstructor = new(
        SkipAutoInitializationOnPropertyConstructorContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SkipAutoInitializationOnProperty.ctor");

    public static readonly Generated SkipAutoInitializationOnPropertyWithAgeExtensions = new(
        SkipAutoInitializationOnPropertyWithAgeExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SkipAutoInitializationOnPropertyExtensions.WithAge");

    public static readonly Generated SkipAutoInitializationOnPropertyWithDependencyExtensions = new(
        SkipAutoInitializationOnPropertyWithDependencyExtensionsContent,
        typeof(RecordGenerator),
        "Fluentify.Records.Testing.SkipAutoInitializationOnPropertyExtensions.WithDependency");

    private const string SkipAutoInitializationOnPropertyConstructorContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            partial record SkipAutoInitializationOnProperty
            {
                #pragma warning disable CS8604

                #if NET7_0_OR_GREATER
                [global::System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
                #endif
                public SkipAutoInitializationOnProperty()
                    : this(default(int), default(global::Fluentify.Records.Testing.SkipAutoInitializationOnProperty.Dependent))
                {
                }

                #pragma warning restore CS8604
            }
        }

        #pragma warning restore CS8625
        #nullable restore
        """;

    private const string SkipAutoInitializationOnPropertyWithAgeExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class SkipAutoInitializationOnPropertyExtensions
            {
                public static global::Fluentify.Records.Testing.SkipAutoInitializationOnProperty WithAge(this global::Fluentify.Records.Testing.SkipAutoInitializationOnProperty subject, int value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

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

    private const string SkipAutoInitializationOnPropertyWithDependencyExtensionsContent = """
        #nullable enable
        #pragma warning disable CS8625

        namespace Fluentify.Records.Testing
        {
            public static partial class SkipAutoInitializationOnPropertyExtensions
            {
                public static global::Fluentify.Records.Testing.SkipAutoInitializationOnProperty WithDependency(this global::Fluentify.Records.Testing.SkipAutoInitializationOnProperty subject, global::Fluentify.Records.Testing.SkipAutoInitializationOnProperty.Dependent instance, global::System.Func<global::Fluentify.Records.Testing.SkipAutoInitializationOnProperty.Dependent, global::Fluentify.Records.Testing.SkipAutoInitializationOnProperty.Dependent> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithDependency(instance);
                }

                public static global::Fluentify.Records.Testing.SkipAutoInitializationOnProperty WithDependency(this global::Fluentify.Records.Testing.SkipAutoInitializationOnProperty subject, global::System.Func<global::Fluentify.Records.Testing.SkipAutoInitializationOnProperty.Dependent, global::Fluentify.Records.Testing.SkipAutoInitializationOnProperty.Dependent> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = subject.Dependency;

                    if (global::System.Object.ReferenceEquals(instance, null))
                    {
                        throw new global::System.NotSupportedException();
                    }

                    instance = builder(instance);

                    return subject.WithDependency(instance);
                }

                public static global::Fluentify.Records.Testing.SkipAutoInitializationOnProperty WithDependency(this global::Fluentify.Records.Testing.SkipAutoInitializationOnProperty subject, global::Fluentify.Records.Testing.SkipAutoInitializationOnProperty.Dependent value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

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