namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string DescriptorOnOptionalContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class DescriptorOnOptional
            {
                public int Age { get; set; }

                public string Name { get; set; }

                [Descriptor("AttributedWith")]
                public IReadOnlyList<object> Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared DescriptorOnOptional;

    public static readonly Generated DescriptorOnOptionalWithAgeExtensions = new(
        DescriptorOnOptionalWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.DescriptorOnOptionalExtensions.WithAge");

    public static readonly Generated DescriptorOnOptionalAttributedWithExtensions = new(
        DescriptorOnOptionalAttributedWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.DescriptorOnOptionalExtensions.AttributedWith");

    public static readonly Generated DescriptorOnOptionalWithNameExtensions = new(
        DescriptorOnOptionalWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.DescriptorOnOptionalExtensions.WithName");

    public static readonly Generated DescriptorOnOptionalWithExtensions = new(
        DescriptorOnOptionalWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.DescriptorOnOptionalExtensions.With");

    private const string DescriptorOnOptionalWithAgeExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class DescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnOptional WithAge(this global::Fluentify.Classes.Testing.DescriptorOnOptional subject, int value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    return new global::Fluentify.Classes.Testing.DescriptorOnOptional
                    {
                        Age = value,
                        Attributes = subject.Attributes,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string DescriptorOnOptionalAttributedWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class DescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnOptional AttributedWith(this global::Fluentify.Classes.Testing.DescriptorOnOptional subject, global::System.Func<object, object> builder, params object[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    foreach (var value in values)
                    {
                        subject = subject.AttributedWith(value, builder);
                    }

                    return subject;
                }

                public static global::Fluentify.Classes.Testing.DescriptorOnOptional AttributedWith(this global::Fluentify.Classes.Testing.DescriptorOnOptional subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.AttributedWith(instance);
                }

                public static global::Fluentify.Classes.Testing.DescriptorOnOptional AttributedWith(this global::Fluentify.Classes.Testing.DescriptorOnOptional subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.AttributedWith(instance);
                }

                public static global::Fluentify.Classes.Testing.DescriptorOnOptional AttributedWith(this global::Fluentify.Classes.Testing.DescriptorOnOptional subject, params object[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    global::System.Collections.Generic.IReadOnlyList<object> value = values;

                    if (subject.Attributes != null)
                    {
                        value = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Union(subject.Attributes, values));
                    }

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.DescriptorOnOptional
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string DescriptorOnOptionalWithNameExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class DescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnOptional WithName(this global::Fluentify.Classes.Testing.DescriptorOnOptional subject, string value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.DescriptorOnOptional
                    {
                        Age = subject.Age,
                        Attributes = subject.Attributes,
                        Name = value,
                    };
                }
            }
        }
        """;

    private const string DescriptorOnOptionalWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class DescriptorOnOptionalExtensions
            {
                internal static global::Fluentify.Classes.Testing.DescriptorOnOptional With(this global::Fluentify.Classes.Testing.DescriptorOnOptional subject, global::System.Func<int> age = default, global::System.Func<global::System.Collections.Generic.IReadOnlyList<object>> attributes = default, global::System.Func<string> name = default)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    var ageValue = global::System.Object.ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = global::System.Object.ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = global::System.Object.ReferenceEquals(name, null) ? subject.Name : name();

                    global::Fluentify.Internal.Extensions.ThrowIfNull(attributesValue, "attributes");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(nameValue, "name");

                    return new global::Fluentify.Classes.Testing.DescriptorOnOptional
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