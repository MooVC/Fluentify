namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string DescriptorOnIgnoredContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class DescriptorOnIgnored
            {
                public int Age { get; set; }

                [Descriptor("Named"), Ignore]
                public string Name { get; set; }

                public IReadOnlyList<object> Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared DescriptorOnIgnored;

    public static readonly Generated DescriptorOnIgnoredWithAgeExtensions = new(
        DescriptorOnIgnoredWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.DescriptorOnIgnoredExtensions.WithAge");

    public static readonly Generated DescriptorOnIgnoredWithAttributesExtensions = new(
        DescriptorOnIgnoredWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.DescriptorOnIgnoredExtensions.WithAttributes");

    public static readonly Generated DescriptorOnIgnoredWithExtensions = new(
        DescriptorOnIgnoredWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.DescriptorOnIgnoredExtensions.With");

    private const string DescriptorOnIgnoredWithAgeExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class DescriptorOnIgnoredExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnIgnored WithAge(
                    this global::Fluentify.Classes.Testing.DescriptorOnIgnored subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.DescriptorOnIgnored
                    {
                        Age = value,
                        Attributes = subject.Attributes,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string DescriptorOnIgnoredWithAttributesExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class DescriptorOnIgnoredExtensions
            {
                public static global::Fluentify.Classes.Testing.DescriptorOnIgnored WithAttributes(
                    this global::Fluentify.Classes.Testing.DescriptorOnIgnored subject,
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

                public static global::Fluentify.Classes.Testing.DescriptorOnIgnored WithAttributes(
                    this global::Fluentify.Classes.Testing.DescriptorOnIgnored subject,
                    object instance,
                    Func<object, object> builder)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.DescriptorOnIgnored WithAttributes(
                    this global::Fluentify.Classes.Testing.DescriptorOnIgnored subject,
                    Func<object, object> builder)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");
        
                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.DescriptorOnIgnored WithAttributes(
                    this global::Fluentify.Classes.Testing.DescriptorOnIgnored subject,
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

                    return new global::Fluentify.Classes.Testing.DescriptorOnIgnored
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string DescriptorOnIgnoredWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            using System;
            using Fluentify.Internal;

            public static partial class DescriptorOnIgnoredExtensions
            {
                internal static global::Fluentify.Classes.Testing.DescriptorOnIgnored With(
                    this global::Fluentify.Classes.Testing.DescriptorOnIgnored subject,
                    Func<int> age = default,
                    Func<global::System.Collections.Generic.IReadOnlyList<object>> attributes = default,
                    Func<string> name = default)
                {
                    subject.ThrowIfNull("subject");

                    var ageValue = ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    attributesValue.ThrowIfNull("attributes");
                    var nameValue = ReferenceEquals(name, null) ? subject.Name : name();
                    nameValue.ThrowIfNull("name");

                    return new global::Fluentify.Classes.Testing.DescriptorOnIgnored
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