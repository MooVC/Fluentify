namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string SelfDescriptorOnIgnoredContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class SelfDescriptorOnIgnored
            {
                public int Age { get; set; }

                [Descriptor, Ignore]
                public string Name { get; set; }

                public IReadOnlyList<object> Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared SelfDescriptorOnIgnored;

    public static readonly Generated SelfDescriptorOnIgnoredWithAgeExtensions = new(
        SelfDescriptorOnIgnoredWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SelfDescriptorOnIgnoredExtensions.WithAge");

    public static readonly Generated SelfDescriptorOnIgnoredWithAttributesExtensions = new(
        SelfDescriptorOnIgnoredWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SelfDescriptorOnIgnoredExtensions.WithAttributes");

    public static readonly Generated SelfDescriptorOnIgnoredWithExtensions = new(
        SelfDescriptorOnIgnoredWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SelfDescriptorOnIgnoredExtensions.With");

    private const string SelfDescriptorOnIgnoredWithAgeExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SelfDescriptorOnIgnoredExtensions
            {
                public static global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored WithAge(this global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored subject, int value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    return new global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored
                    {
                        Age = value,
                        Attributes = subject.Attributes,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string SelfDescriptorOnIgnoredWithAttributesExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SelfDescriptorOnIgnoredExtensions
            {
                public static global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored WithAttributes(this global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored WithAttributes(this global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored WithAttributes(this global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored WithAttributes(this global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored subject, params object[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    global::System.Collections.Generic.IReadOnlyList<object> value = values;

                    if (subject.Attributes != null)
                    {
                        value = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Union(subject.Attributes, values));
                    }

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string SelfDescriptorOnIgnoredWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class SelfDescriptorOnIgnoredExtensions
            {
                internal static global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored With(this global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored subject, global::System.Func<int> age = default, global::System.Func<global::System.Collections.Generic.IReadOnlyList<object>> attributes = default, global::System.Func<string> name = default)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    var ageValue = global::System.Object.ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = global::System.Object.ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = global::System.Object.ReferenceEquals(name, null) ? subject.Name : name();

                    global::Fluentify.Internal.Extensions.ThrowIfNull(attributesValue, "attributes");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(nameValue, "name");

                    return new global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored
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