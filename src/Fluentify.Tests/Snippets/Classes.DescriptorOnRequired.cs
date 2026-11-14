namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string DescriptorOnRequiredContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class DescriptorOnRequired
            {
                [Descriptor("Aged")]
                public int Age { get; set; }

                public string Name { get; set; }

                public IReadOnlyList<object> Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared DescriptorOnRequired;

    public static readonly Generated DescriptorOnRequiredAgedExtensions = new(
        DescriptorOnRequiredAgedExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.DescriptorOnRequiredExtensions.Aged");

    public static readonly Generated DescriptorOnRequiredWithAttributesExtensions = new(
        DescriptorOnRequiredWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.DescriptorOnRequiredExtensions.WithAttributes");

    public static readonly Generated DescriptorOnRequiredWithNameExtensions = new(
        DescriptorOnRequiredWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.DescriptorOnRequiredExtensions.WithName");

    public static readonly Generated DescriptorOnRequiredWithExtensions = new(
        DescriptorOnRequiredWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.DescriptorOnRequiredExtensions.With");

    private const string DescriptorOnRequiredAgedExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class DescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnRequired Aged(this global::Fluentify.Classes.Testing.DescriptorOnRequired subject, int value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    return new global::Fluentify.Classes.Testing.DescriptorOnRequired
                    {
                        Age = value,
                        Attributes = subject.Attributes,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string DescriptorOnRequiredWithAttributesExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class DescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnRequired WithAttributes(this global::Fluentify.Classes.Testing.DescriptorOnRequired subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Classes.Testing.DescriptorOnRequired WithAttributes(this global::Fluentify.Classes.Testing.DescriptorOnRequired subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.DescriptorOnRequired WithAttributes(this global::Fluentify.Classes.Testing.DescriptorOnRequired subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.DescriptorOnRequired WithAttributes(this global::Fluentify.Classes.Testing.DescriptorOnRequired subject, params object[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    global::System.Collections.Generic.IReadOnlyList<object> value = values;

                    if (subject.Attributes != null)
                    {
                        value = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Union(subject.Attributes, values));
                    }

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.DescriptorOnRequired
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string DescriptorOnRequiredWithNameExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class DescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnRequired WithName(this global::Fluentify.Classes.Testing.DescriptorOnRequired subject, string value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.DescriptorOnRequired
                    {
                        Age = subject.Age,
                        Attributes = subject.Attributes,
                        Name = value,
                    };
                }
            }
        }
        """;

    private const string DescriptorOnRequiredWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class DescriptorOnRequiredExtensions
            {
                internal static global::Fluentify.Classes.Testing.DescriptorOnRequired With(this global::Fluentify.Classes.Testing.DescriptorOnRequired subject, global::System.Func<int> age = default, global::System.Func<global::System.Collections.Generic.IReadOnlyList<object>> attributes = default, global::System.Func<string> name = default)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    var ageValue = global::System.Object.ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = global::System.Object.ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = global::System.Object.ReferenceEquals(name, null) ? subject.Name : name();

                    global::Fluentify.Internal.Extensions.ThrowIfNull(attributesValue, "attributes");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(nameValue, "name");

                    return new global::Fluentify.Classes.Testing.DescriptorOnRequired
                    {
                        Age = ageValue,
                        Attributes = attributesValue,
                        Name = nameValue,
                    };
                }
            }
        }
        """;
}