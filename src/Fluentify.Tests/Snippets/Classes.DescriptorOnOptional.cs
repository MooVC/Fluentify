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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class DescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnOptional WithAge(
                    this global::Fluentify.Classes.Testing.DescriptorOnOptional subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class DescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnOptional AttributedWith(
                    this global::Fluentify.Classes.Testing.DescriptorOnOptional subject,
                    Func<object, object> builder,
                    params object[] values)
                {
                    subject.ThrowIfNull("subject");
                    builder.ThrowIfNull("builder");

                    foreach (var value in values)
                    {
                        subject = subject.AttributedWith(value, builder);
                    }

                    return subject;
                }

                public static global::Fluentify.Classes.Testing.DescriptorOnOptional AttributedWith(
                    this global::Fluentify.Classes.Testing.DescriptorOnOptional subject,
                    object instance,
                    Func<object, object> builder)
                {
                    subject.ThrowIfNull("subject");
                    builder.ThrowIfNull("builder");

                    instance = builder(instance);

                    return subject.AttributedWith(instance);
                }

                public static global::Fluentify.Classes.Testing.DescriptorOnOptional AttributedWith(
                    this global::Fluentify.Classes.Testing.DescriptorOnOptional subject,
                    Func<object, object> builder)
                {
                    subject.ThrowIfNull("subject");
                    builder.ThrowIfNull("builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.AttributedWith(instance);
                }

                public static global::Fluentify.Classes.Testing.DescriptorOnOptional AttributedWith(
                    this global::Fluentify.Classes.Testing.DescriptorOnOptional subject,
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
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class DescriptorOnOptionalExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnOptional WithName(
                    this global::Fluentify.Classes.Testing.DescriptorOnOptional subject,
                    string value)
                {
                    subject.ThrowIfNull("subject");

                    value.ThrowIfNull("value");

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
            using System;
            using Fluentify.Internal;

            public static partial class DescriptorOnOptionalExtensions
            {
                internal static global::Fluentify.Classes.Testing.DescriptorOnOptional With(
                    this global::Fluentify.Classes.Testing.DescriptorOnOptional subject,
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