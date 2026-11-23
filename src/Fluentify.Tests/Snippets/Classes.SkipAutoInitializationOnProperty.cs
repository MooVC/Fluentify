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
        namespace Fluentify.Classes.Testing
        {
            public static partial class SkipAutoInitializationOnPropertyExtensions
            {
                public static global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty WithAge(this global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty subject, int value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    return new global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty
                    {
                        Age = value,
                        Dependency = subject.Dependency,
                    };
                }
            }
        }
        """;

    private const string SkipAutoInitializationOnPropertyWithDependencyExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SkipAutoInitializationOnPropertyExtensions
            {
                public static global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty WithDependency(this global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty subject, global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty.Dependent instance, global::System.Func<global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty.Dependent, global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty.Dependent> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithDependency(instance);
                }

                public static global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty WithDependency(this global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty subject, global::System.Func<global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty.Dependent, global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty.Dependent> builder)
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

                public static global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty WithDependency(this global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty subject, global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty.Dependent value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty
                    {
                        Age = subject.Age,
                        Dependency = value,
                    };
                }
            }
        }
        """;

    private const string SkipAutoInitializationOnPropertyWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SkipAutoInitializationOnPropertyExtensions
            {
                internal static global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty With(this global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty subject, global::System.Func<int> age = default, global::System.Func<global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty.Dependent> dependency = default)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    var ageValue = global::System.Object.ReferenceEquals(age, null) ? subject.Age : age();
                    var dependencyValue = global::System.Object.ReferenceEquals(dependency, null) ? subject.Dependency : dependency();

                    global::Fluentify.Internal.Extensions.ThrowIfNull(dependencyValue, "dependency");

                    return new global::Fluentify.Classes.Testing.SkipAutoInitializationOnProperty
                    {
                        Age = ageValue,
                        Dependency = dependencyValue,
                    };
                }
            }
        }
        """;
}