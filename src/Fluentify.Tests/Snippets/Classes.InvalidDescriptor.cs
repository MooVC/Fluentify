namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string InvalidDescriptorContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class InvalidDescriptor
            {
                [Descriptor(" ")]
                public int Age { get; set; }

                [Descriptor("With Name")]
                public string Name { get; set; }

                [Descriptor("&Invalid")]
                public IReadOnlyList<object> Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared InvalidDescriptor;

    public static readonly Generated InvalidDescriptorWithAgeExtensions = new(
        InvalidDescriptorWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.InvalidDescriptorExtensions.WithAge");

    public static readonly Generated InvalidDescriptorWithAttributesExtensions = new(
        InvalidDescriptorWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.InvalidDescriptorExtensions.WithAttributes");

    public static readonly Generated InvalidDescriptorWithNameExtensions = new(
        InvalidDescriptorWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.InvalidDescriptorExtensions.WithName");

    public static readonly Generated InvalidDescriptorWithExtensions = new(
        InvalidDescriptorWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.InvalidDescriptorExtensions.With");

    private const string InvalidDescriptorWithAgeExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class InvalidDescriptorExtensions
            {
                public static global::Fluentify.Classes.Testing.InvalidDescriptor WithAge(this global::Fluentify.Classes.Testing.InvalidDescriptor subject, int value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    return new global::Fluentify.Classes.Testing.InvalidDescriptor
                    {
                        Age = value,
                        Attributes = subject.Attributes,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string InvalidDescriptorWithAttributesExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class InvalidDescriptorExtensions
            {
                public static global::Fluentify.Classes.Testing.InvalidDescriptor WithAttributes(this global::Fluentify.Classes.Testing.InvalidDescriptor subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Classes.Testing.InvalidDescriptor WithAttributes(this global::Fluentify.Classes.Testing.InvalidDescriptor subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.InvalidDescriptor WithAttributes(this global::Fluentify.Classes.Testing.InvalidDescriptor subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.InvalidDescriptor WithAttributes(this global::Fluentify.Classes.Testing.InvalidDescriptor subject, params object[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    global::System.Collections.Generic.IReadOnlyList<object> value = values;

                    if (subject.Attributes != null)
                    {
                        value = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Union(subject.Attributes, values));
                    }

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.InvalidDescriptor
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string InvalidDescriptorWithNameExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class InvalidDescriptorExtensions
            {
                public static global::Fluentify.Classes.Testing.InvalidDescriptor WithName(this global::Fluentify.Classes.Testing.InvalidDescriptor subject, string value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.InvalidDescriptor
                    {
                        Age = subject.Age,
                        Attributes = subject.Attributes,
                        Name = value,
                    };
                }
            }
        }
        """;

    private const string InvalidDescriptorWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class InvalidDescriptorExtensions
            {
                internal static global::Fluentify.Classes.Testing.InvalidDescriptor With(this global::Fluentify.Classes.Testing.InvalidDescriptor subject, global::System.Func<int> age = default, global::System.Func<global::System.Collections.Generic.IReadOnlyList<object>> attributes = default, global::System.Func<string> name = default)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    var ageValue = global::System.Object.ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = global::System.Object.ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = global::System.Object.ReferenceEquals(name, null) ? subject.Name : name();

                    global::Fluentify.Internal.Extensions.ThrowIfNull(attributesValue, "attributes");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(nameValue, "name");

                    return new global::Fluentify.Classes.Testing.InvalidDescriptor
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