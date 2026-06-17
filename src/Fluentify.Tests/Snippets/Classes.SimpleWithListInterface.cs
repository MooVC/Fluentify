namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string SimpleWithListInterfaceContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class SimpleWithListInterface
            {
                public int Age { get; set; }

                public string Name { get; set; }

                public IList<object> Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared SimpleWithListInterface;

    public static readonly Generated SimpleWithListInterfaceWithAgeExtensions = new(
        SimpleWithListInterfaceWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SimpleWithListInterfaceExtensions.WithAge");

    public static readonly Generated SimpleWithListInterfaceWithAttributesExtensions = new(
        SimpleWithListInterfaceWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SimpleWithListInterfaceExtensions.WithAttributes");

    public static readonly Generated SimpleWithListInterfaceWithNameExtensions = new(
        SimpleWithListInterfaceWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SimpleWithListInterfaceExtensions.WithName");

    public static readonly Generated SimpleWithListInterfaceWithExtensions = new(
        SimpleWithListInterfaceWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SimpleWithListInterfaceExtensions.With");

    private const string SimpleWithListInterfaceWithAgeExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SimpleWithListInterfaceExtensions
            {
                public static global::Fluentify.Classes.Testing.SimpleWithListInterface WithAge(
                    this global::Fluentify.Classes.Testing.SimpleWithListInterface subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SimpleWithListInterface
                    {
                        Age = value,
                        Attributes = subject.Attributes,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string SimpleWithListInterfaceWithAttributesExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SimpleWithListInterfaceExtensions
            {
                public static global::Fluentify.Classes.Testing.SimpleWithListInterface WithAttributes(
                    this global::Fluentify.Classes.Testing.SimpleWithListInterface subject,
                    Func<object, object> builder,
                    params object[] values)
                {
                    subject.ThrowIfNull("subject");
                    builder.ThrowIfNull("builder");
                    values.ThrowIfNull("values");

                    foreach (var value in values)
                    {
                        subject = subject.WithAttributes(value, builder);
                    }

                    return subject;
                }

                public static global::Fluentify.Classes.Testing.SimpleWithListInterface WithAttributes(
                    this global::Fluentify.Classes.Testing.SimpleWithListInterface subject,
                    object instance,
                    Func<object, object> builder)
                {
                    subject.ThrowIfNull("subject");
                    builder.ThrowIfNull("builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.SimpleWithListInterface WithAttributes(
                    this global::Fluentify.Classes.Testing.SimpleWithListInterface subject,
                    Func<object, object> builder)
                {
                    subject.ThrowIfNull("subject");
                    builder.ThrowIfNull("builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.SimpleWithListInterface WithAttributes(
                    this global::Fluentify.Classes.Testing.SimpleWithListInterface subject,
                    params object[] values)
                {
                    subject.ThrowIfNull("subject");
                    values.ThrowIfNull("values");

                    global::System.Collections.Generic.IList<object> value = values.ToList();

                    if (subject.Attributes != null)
                    {
                        value = subject.Attributes
                            .Union(values)
                            .ToList();
                    }

                    value.ThrowIfNull("value");

                    return new global::Fluentify.Classes.Testing.SimpleWithListInterface
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string SimpleWithListInterfaceWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            using System;
            using Fluentify.Internal;

            public static partial class SimpleWithListInterfaceExtensions
            {
                internal static global::Fluentify.Classes.Testing.SimpleWithListInterface With(
                    this global::Fluentify.Classes.Testing.SimpleWithListInterface subject,
                    Func<int> age = default,
                    Func<global::System.Collections.Generic.IList<object>> attributes = default,
                    Func<string> name = default)
                {
                    subject.ThrowIfNull("subject");

                    var ageValue = ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = ReferenceEquals(name, null) ? subject.Name : name();

                    attributesValue.ThrowIfNull("attributes");
                    nameValue.ThrowIfNull("name");

                    return new global::Fluentify.Classes.Testing.SimpleWithListInterface
                    {
                        Age = ageValue,
                        Attributes = attributesValue,
                        Name = nameValue,
                    };
                }
            }
        }
        """;

    private const string SimpleWithListInterfaceWithNameExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SimpleWithListInterfaceExtensions
            {
                public static global::Fluentify.Classes.Testing.SimpleWithListInterface WithName(
                    this global::Fluentify.Classes.Testing.SimpleWithListInterface subject,
                    string value)
                {
                    subject.ThrowIfNull("subject");

                    value.ThrowIfNull("value");

                    return new global::Fluentify.Classes.Testing.SimpleWithListInterface
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