namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string SimpleWithCollectionInterfaceContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class SimpleWithCollectionInterface
            {
                public int Age { get; set; }

                public string Name { get; set; }

                public ICollection<object> Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared SimpleWithCollectionInterface;

    public static readonly Generated SimpleWithCollectionInterfaceWithAgeExtensions = new(
        SimpleWithCollectionInterfaceWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SimpleWithCollectionInterfaceExtensions.WithAge");

    public static readonly Generated SimpleWithCollectionInterfaceWithAttributesExtensions = new(
        SimpleWithCollectionInterfaceWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SimpleWithCollectionInterfaceExtensions.WithAttributes");

    public static readonly Generated SimpleWithCollectionInterfaceWithNameExtensions = new(
        SimpleWithCollectionInterfaceWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SimpleWithCollectionInterfaceExtensions.WithName");

    public static readonly Generated SimpleWithCollectionInterfaceWithExtensions = new(
        SimpleWithCollectionInterfaceWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SimpleWithCollectionInterfaceExtensions.With");

    private const string SimpleWithCollectionInterfaceWithAgeExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SimpleWithCollectionInterfaceExtensions
            {
                public static global::Fluentify.Classes.Testing.SimpleWithCollectionInterface WithAge(this global::Fluentify.Classes.Testing.SimpleWithCollectionInterface subject, int value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    return new global::Fluentify.Classes.Testing.SimpleWithCollectionInterface
                    {
                        Age = value,
                        Attributes = subject.Attributes,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string SimpleWithCollectionInterfaceWithAttributesExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SimpleWithCollectionInterfaceExtensions
            {
                public static global::Fluentify.Classes.Testing.SimpleWithCollectionInterface WithAttributes(this global::Fluentify.Classes.Testing.SimpleWithCollectionInterface subject, global::System.Func<object, object> builder, params object[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    foreach (var value in values)
                    {
                        subject = subject.WithAttributes(value, builder);
                    }

                    return subject;
                }

                public static global::Fluentify.Classes.Testing.SimpleWithCollectionInterface WithAttributes(this global::Fluentify.Classes.Testing.SimpleWithCollectionInterface subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.SimpleWithCollectionInterface WithAttributes(this global::Fluentify.Classes.Testing.SimpleWithCollectionInterface subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.SimpleWithCollectionInterface WithAttributes(this global::Fluentify.Classes.Testing.SimpleWithCollectionInterface subject, params object[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    global::System.Collections.Generic.ICollection<object> value = global::System.Linq.Enumerable.ToList(values);

                    if (subject.Attributes != null)
                    {
                        value = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Union(subject.Attributes, values));
                    }

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.SimpleWithCollectionInterface
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string SimpleWithCollectionInterfaceWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SimpleWithCollectionInterfaceExtensions
            {
                internal static global::Fluentify.Classes.Testing.SimpleWithCollectionInterface With(this global::Fluentify.Classes.Testing.SimpleWithCollectionInterface subject, global::System.Func<int> age = default, global::System.Func<global::System.Collections.Generic.ICollection<object>> attributes = default, global::System.Func<string> name = default)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    var ageValue = global::System.Object.ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = global::System.Object.ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = global::System.Object.ReferenceEquals(name, null) ? subject.Name : name();

                    global::Fluentify.Internal.Extensions.ThrowIfNull(attributesValue, "attributes");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(nameValue, "name");

                    return new global::Fluentify.Classes.Testing.SimpleWithCollectionInterface
                    {
                        Age = ageValue,
                        Attributes = attributesValue,
                        Name = nameValue,
                    };
                }
            }
        }
        """;

    private const string SimpleWithCollectionInterfaceWithNameExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SimpleWithCollectionInterfaceExtensions
            {
                public static global::Fluentify.Classes.Testing.SimpleWithCollectionInterface WithName(this global::Fluentify.Classes.Testing.SimpleWithCollectionInterface subject, string value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.SimpleWithCollectionInterface
                    {
                        Age = subject.Age,
                        Attributes = subject.Attributes,
                        Name = value,
                    };
                }
            }
        }
        """;
}