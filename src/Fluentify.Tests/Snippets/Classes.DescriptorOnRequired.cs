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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class DescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnRequired Aged(
                    this global::Fluentify.Classes.Testing.DescriptorOnRequired subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class DescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnRequired WithAttributes(
                    this global::Fluentify.Classes.Testing.DescriptorOnRequired subject,
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

                public static global::Fluentify.Classes.Testing.DescriptorOnRequired WithAttributes(
                    this global::Fluentify.Classes.Testing.DescriptorOnRequired subject,
                    object instance,
                    Func<object, object> builder)
                {
                    subject.ThrowIfNull("subject");
                    builder.ThrowIfNull("builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.DescriptorOnRequired WithAttributes(
                    this global::Fluentify.Classes.Testing.DescriptorOnRequired subject,
                    Func<object, object> builder)
                {
                    subject.ThrowIfNull("subject");
                    builder.ThrowIfNull("builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.DescriptorOnRequired WithAttributes(
                    this global::Fluentify.Classes.Testing.DescriptorOnRequired subject,
                    params object[] values)
                {
                    subject.ThrowIfNull("subject");

                    global::System.Collections.Generic.IReadOnlyList<object> value = values;

                    if (subject.Attributes != null)
                    {
                        value = subject.Attributes
                            .Union(values)
                            .ToArray();
                    }

                    value.ThrowIfNull("value");

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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class DescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnRequired WithName(
                    this global::Fluentify.Classes.Testing.DescriptorOnRequired subject,
                    string value)
                {
                    subject.ThrowIfNull("subject");

                    value.ThrowIfNull("value");

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
            using System;
            using Fluentify.Internal;

            public static partial class DescriptorOnRequiredExtensions
            {
                internal static global::Fluentify.Classes.Testing.DescriptorOnRequired With(
                    this global::Fluentify.Classes.Testing.DescriptorOnRequired subject,
                    Func<int> age = default,
                    Func<global::System.Collections.Generic.IReadOnlyList<object>> attributes = default,
                    Func<string> name = default)
                {
                    subject.ThrowIfNull("subject");

                    var ageValue = ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = ReferenceEquals(name, null) ? subject.Name : name();

                    attributesValue.ThrowIfNull("attributes");
                    nameValue.ThrowIfNull("name");

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