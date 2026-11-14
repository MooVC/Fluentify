namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string SelfDescriptorOnOptionalContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class SelfDescriptorOnOptional
            {
                public int Age { get; set; }

                public string Name { get; set; }

                [Descriptor]
                public IReadOnlyList<object> Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared SelfDescriptorOnOptional;

    public static readonly Generated SelfDescriptorOnOptionalWithAgeExtensions = new(
        SelfDescriptorOnOptionalWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SelfDescriptorOnOptionalExtensions.WithAge");

    public static readonly Generated SelfDescriptorOnOptionalAttributesExtensions = new(
        SelfDescriptorOnOptionalAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SelfDescriptorOnOptionalExtensions.Attributes");

    public static readonly Generated SelfDescriptorOnOptionalWithNameExtensions = new(
        SelfDescriptorOnOptionalWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SelfDescriptorOnOptionalExtensions.WithName");

    public static readonly Generated SelfDescriptorOnOptionalWithExtensions = new(
        SelfDescriptorOnOptionalWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SelfDescriptorOnOptionalExtensions.With");

    private const string SelfDescriptorOnOptionalWithAgeExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SelfDescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Classes.Testing.SelfDescriptorOnOptional WithAge(this global::Fluentify.Classes.Testing.SelfDescriptorOnOptional subject, int value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    return new global::Fluentify.Classes.Testing.SelfDescriptorOnOptional
                    {
                        Age = value,
                        Attributes = subject.Attributes,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string SelfDescriptorOnOptionalAttributesExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SelfDescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Classes.Testing.SelfDescriptorOnOptional Attributes(this global::Fluentify.Classes.Testing.SelfDescriptorOnOptional subject, global::System.Func<object, object> builder, params object[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    foreach (var value in values)
                    {
                        subject = subject.Attributes(value, builder);
                    }

                    return subject;
                }

                public static global::Fluentify.Classes.Testing.SelfDescriptorOnOptional Attributes(this global::Fluentify.Classes.Testing.SelfDescriptorOnOptional subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.Attributes(instance);
                }

                public static global::Fluentify.Classes.Testing.SelfDescriptorOnOptional Attributes(this global::Fluentify.Classes.Testing.SelfDescriptorOnOptional subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.Attributes(instance);
                }

                public static global::Fluentify.Classes.Testing.SelfDescriptorOnOptional Attributes(this global::Fluentify.Classes.Testing.SelfDescriptorOnOptional subject, params object[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    global::System.Collections.Generic.IReadOnlyList<object> value = values;

                    if (subject.Attributes != null)
                    {
                        value = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Union(subject.Attributes, values));
                    }

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.SelfDescriptorOnOptional
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string SelfDescriptorOnOptionalWithNameExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SelfDescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Classes.Testing.SelfDescriptorOnOptional WithName(this global::Fluentify.Classes.Testing.SelfDescriptorOnOptional subject, string value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.SelfDescriptorOnOptional
                    {
                        Age = subject.Age,
                        Attributes = subject.Attributes,
                        Name = value,
                    };
                }
            }
        }
        """;

    private const string SelfDescriptorOnOptionalWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SelfDescriptorOnOptionalExtensions
            {
                internal static global::Fluentify.Classes.Testing.SelfDescriptorOnOptional With(this global::Fluentify.Classes.Testing.SelfDescriptorOnOptional subject, global::System.Func<int> age = default, global::System.Func<global::System.Collections.Generic.IReadOnlyList<object>> attributes = default, global::System.Func<string> name = default)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    var ageValue = global::System.Object.ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = global::System.Object.ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = global::System.Object.ReferenceEquals(name, null) ? subject.Name : name();

                    global::Fluentify.Internal.Extensions.ThrowIfNull(attributesValue, "attributes");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(nameValue, "name");

                    return new global::Fluentify.Classes.Testing.SelfDescriptorOnOptional
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