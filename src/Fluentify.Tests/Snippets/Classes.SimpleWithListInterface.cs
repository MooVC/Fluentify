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
            public static partial class SimpleWithListInterfaceExtensions
            {
                public static global::Fluentify.Classes.Testing.SimpleWithListInterface WithAge(this global::Fluentify.Classes.Testing.SimpleWithListInterface subject, int value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

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
            public static partial class SimpleWithListInterfaceExtensions
            {
                public static global::Fluentify.Classes.Testing.SimpleWithListInterface WithAttributes(this global::Fluentify.Classes.Testing.SimpleWithListInterface subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Classes.Testing.SimpleWithListInterface WithAttributes(this global::Fluentify.Classes.Testing.SimpleWithListInterface subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.SimpleWithListInterface WithAttributes(this global::Fluentify.Classes.Testing.SimpleWithListInterface subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.SimpleWithListInterface WithAttributes(this global::Fluentify.Classes.Testing.SimpleWithListInterface subject, params object[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    global::System.Collections.Generic.IList<object> value = global::System.Linq.Enumerable.ToList(values);

                    if (subject.Attributes != null)
                    {
                        value = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Union(subject.Attributes, values));
                    }

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

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
            public static partial class SimpleWithListInterfaceExtensions
            {
                internal static global::Fluentify.Classes.Testing.SimpleWithListInterface With(this global::Fluentify.Classes.Testing.SimpleWithListInterface subject, global::System.Func<int> age = default, global::System.Func<global::System.Collections.Generic.IList<object>> attributes = default, global::System.Func<string> name = default)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    var ageValue = global::System.Object.ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = global::System.Object.ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = global::System.Object.ReferenceEquals(name, null) ? subject.Name : name();

                    global::Fluentify.Internal.Extensions.ThrowIfNull(attributesValue, "attributes");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(nameValue, "name");

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
            public static partial class SimpleWithListInterfaceExtensions
            {
                public static global::Fluentify.Classes.Testing.SimpleWithListInterface WithName(this global::Fluentify.Classes.Testing.SimpleWithListInterface subject, string value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

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