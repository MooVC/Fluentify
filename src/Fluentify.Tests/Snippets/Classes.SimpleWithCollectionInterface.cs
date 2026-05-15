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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SimpleWithCollectionInterfaceExtensions
            {
                public static global::Fluentify.Classes.Testing.SimpleWithCollectionInterface WithAge(
                    this global::Fluentify.Classes.Testing.SimpleWithCollectionInterface subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SimpleWithCollectionInterfaceExtensions
            {
                public static global::Fluentify.Classes.Testing.SimpleWithCollectionInterface WithAttributes(
                    this global::Fluentify.Classes.Testing.SimpleWithCollectionInterface subject,
                    Func<object, object> builder,
                    params object[] values)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    foreach (var value in values)
                    {
                        subject = subject.WithAttributes(value, builder);
                    }

                    return subject;
                }

                public static global::Fluentify.Classes.Testing.SimpleWithCollectionInterface WithAttributes(
                    this global::Fluentify.Classes.Testing.SimpleWithCollectionInterface subject,
                    object instance,
                    Func<object, object> builder)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.SimpleWithCollectionInterface WithAttributes(
                    this global::Fluentify.Classes.Testing.SimpleWithCollectionInterface subject,
                    Func<object, object> builder)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.SimpleWithCollectionInterface WithAttributes(
                    this global::Fluentify.Classes.Testing.SimpleWithCollectionInterface subject,
                    params object[] values)
                {
                    subject.ThrowIfNull("subject");

                    global::System.Collections.Generic.ICollection<object> value = values.ToList();

                    if (subject.Attributes != null)
                    {
                        value = subject.Attributes
                            .Union(values)
                            .ToList();
                    }

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
            using System;
            using Fluentify.Internal;

            public static partial class SimpleWithCollectionInterfaceExtensions
            {
                internal static global::Fluentify.Classes.Testing.SimpleWithCollectionInterface With(
                    this global::Fluentify.Classes.Testing.SimpleWithCollectionInterface subject,
                    Func<int> age = default,
                    Func<global::System.Collections.Generic.ICollection<object>> attributes = default,
                    Func<string> name = default)
                {
                    subject.ThrowIfNull("subject");

                    var ageValue = ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = ReferenceEquals(name, null) ? subject.Name : name();

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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SimpleWithCollectionInterfaceExtensions
            {
                public static global::Fluentify.Classes.Testing.SimpleWithCollectionInterface WithName(
                    this global::Fluentify.Classes.Testing.SimpleWithCollectionInterface subject,
                    string value)
                {
                    subject.ThrowIfNull("subject");

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