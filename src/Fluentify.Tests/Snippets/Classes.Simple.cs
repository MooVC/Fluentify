namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string SimpleContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class Simple
            {
                public int Age { get; set; }

                public string Name { get; set; }

                public IReadOnlyList<object> Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared Simple;

    public static readonly Generated SimpleWithAgeExtensions = new(
        SimpleWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SimpleExtensions.WithAge");

    public static readonly Generated SimpleWithAttributesExtensions = new(
        SimpleWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SimpleExtensions.WithAttributes");

    public static readonly Generated SimpleWithNameExtensions = new(
        SimpleWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SimpleExtensions.WithName");

    public static readonly Generated SimpleWithExtensions = new(
        SimpleWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SimpleExtensions.With");

    private const string SimpleWithAgeExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SimpleExtensions
            {
                public static global::Fluentify.Classes.Testing.Simple WithAge(this global::Fluentify.Classes.Testing.Simple subject, int value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    return new global::Fluentify.Classes.Testing.Simple
                    {
                        Age = value,
                        Attributes = subject.Attributes,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string SimpleWithAttributesExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SimpleExtensions
            {
                public static global::Fluentify.Classes.Testing.Simple WithAttributes(this global::Fluentify.Classes.Testing.Simple subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Classes.Testing.Simple WithAttributes(this global::Fluentify.Classes.Testing.Simple subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.Simple WithAttributes(this global::Fluentify.Classes.Testing.Simple subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.Simple WithAttributes(this global::Fluentify.Classes.Testing.Simple subject, params object[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    global::System.Collections.Generic.IReadOnlyList<object> value = values;

                    if (subject.Attributes != null)
                    {
                        value = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Union(subject.Attributes, values));
                    }

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.Simple
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string SimpleWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SimpleExtensions
            {
                internal static global::Fluentify.Classes.Testing.Simple With(this global::Fluentify.Classes.Testing.Simple subject, global::System.Func<int> age = default, global::System.Func<global::System.Collections.Generic.IReadOnlyList<object>> attributes = default, global::System.Func<string> name = default)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    var ageValue = global::System.Object.ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = global::System.Object.ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = global::System.Object.ReferenceEquals(name, null) ? subject.Name : name();

                    global::Fluentify.Internal.Extensions.ThrowIfNull(attributesValue, "attributes");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(nameValue, "name");

                    return new global::Fluentify.Classes.Testing.Simple
                    {
                        Age = ageValue,
                        Attributes = attributesValue,
                        Name = nameValue,
                    };
                }
            }
        }
        """;

    private const string SimpleWithNameExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SimpleExtensions
            {
                public static global::Fluentify.Classes.Testing.Simple WithName(this global::Fluentify.Classes.Testing.Simple subject, string value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.Simple
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