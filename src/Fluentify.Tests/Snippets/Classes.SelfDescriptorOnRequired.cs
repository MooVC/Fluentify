namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string SelfDescriptorOnRequiredContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class SelfDescriptorOnRequired
            {
                [Descriptor]
                public int Age { get; set; }

                [Descriptor("")]
                public string Name { get; set; }

                public IReadOnlyList<object> Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared SelfDescriptorOnRequired;

    public static readonly Generated SelfDescriptorOnRequiredAgeExtensions = new(
        SelfDescriptorOnRequiredAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SelfDescriptorOnRequiredExtensions.Age");

    public static readonly Generated SelfDescriptorOnRequiredWithAttributesExtensions = new(
        SelfDescriptorOnRequiredWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SelfDescriptorOnRequiredExtensions.WithAttributes");

    public static readonly Generated SelfDescriptorOnRequiredWithExtensions = new(
        SelfDescriptorOnRequiredWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SelfDescriptorOnRequiredExtensions.With");

    public static readonly Generated SelfDescriptorOnRequiredNameExtensions = new(
        SelfDescriptorOnRequiredNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SelfDescriptorOnRequiredExtensions.Name");

    private const string SelfDescriptorOnRequiredAgeExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SelfDescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Classes.Testing.SelfDescriptorOnRequired Age(this global::Fluentify.Classes.Testing.SelfDescriptorOnRequired subject, int value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    return new global::Fluentify.Classes.Testing.SelfDescriptorOnRequired
                    {
                        Age = value,
                        Attributes = subject.Attributes,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string SelfDescriptorOnRequiredWithAttributesExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SelfDescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Classes.Testing.SelfDescriptorOnRequired WithAttributes(this global::Fluentify.Classes.Testing.SelfDescriptorOnRequired subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Classes.Testing.SelfDescriptorOnRequired WithAttributes(this global::Fluentify.Classes.Testing.SelfDescriptorOnRequired subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.SelfDescriptorOnRequired WithAttributes(this global::Fluentify.Classes.Testing.SelfDescriptorOnRequired subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.SelfDescriptorOnRequired WithAttributes(this global::Fluentify.Classes.Testing.SelfDescriptorOnRequired subject, params object[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    global::System.Collections.Generic.IReadOnlyList<object> value = values;

                    if (subject.Attributes != null)
                    {
                        value = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Union(subject.Attributes, values));
                    }

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.SelfDescriptorOnRequired
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string SelfDescriptorOnRequiredNameExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SelfDescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Classes.Testing.SelfDescriptorOnRequired Name(this global::Fluentify.Classes.Testing.SelfDescriptorOnRequired subject, string value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.SelfDescriptorOnRequired
                    {
                        Age = subject.Age,
                        Attributes = subject.Attributes,
                        Name = value,
                    };
                }
            }
        }
        """;

    private const string SelfDescriptorOnRequiredWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SelfDescriptorOnRequiredExtensions
            {
                internal static global::Fluentify.Classes.Testing.SelfDescriptorOnRequired With(this global::Fluentify.Classes.Testing.SelfDescriptorOnRequired subject, global::System.Func<int> age = default, global::System.Func<global::System.Collections.Generic.IReadOnlyList<object>> attributes = default, global::System.Func<string> name = default)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    var ageValue = global::System.Object.ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = global::System.Object.ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = global::System.Object.ReferenceEquals(name, null) ? subject.Name : name();

                    global::Fluentify.Internal.Extensions.ThrowIfNull(attributesValue, "attributes");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(nameValue, "name");

                    return new global::Fluentify.Classes.Testing.SelfDescriptorOnRequired
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