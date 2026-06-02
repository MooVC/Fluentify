namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string TwoOfThreeIgnoredContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class TwoOfThreeIgnored
            {
                [Ignore]
                public int Age { get; set; }

                [Ignore]
                public string Name { get; set; }

                public IReadOnlyList<object> Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared TwoOfThreeIgnored;

    public static readonly Generated TwoOfThreeIgnoredWithAttributesExtensions = new(
        TwoOfThreeIgnoredWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.TwoOfThreeIgnoredExtensions.WithAttributes");

    public static readonly Generated TwoOfThreeIgnoredWithExtensions = new(
        TwoOfThreeIgnoredWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.TwoOfThreeIgnoredExtensions.With");

    private const string TwoOfThreeIgnoredWithAttributesExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class TwoOfThreeIgnoredExtensions
            {
                public static global::Fluentify.Classes.Testing.TwoOfThreeIgnored WithAttributes(
                    this global::Fluentify.Classes.Testing.TwoOfThreeIgnored subject,
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

                public static global::Fluentify.Classes.Testing.TwoOfThreeIgnored WithAttributes(
                    this global::Fluentify.Classes.Testing.TwoOfThreeIgnored subject,
                    object instance,
                    Func<object, object> builder)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.TwoOfThreeIgnored WithAttributes(
                    this global::Fluentify.Classes.Testing.TwoOfThreeIgnored subject,
                    Func<object, object> builder)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.TwoOfThreeIgnored WithAttributes(
                    this global::Fluentify.Classes.Testing.TwoOfThreeIgnored subject,
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

                    return new global::Fluentify.Classes.Testing.TwoOfThreeIgnored
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string TwoOfThreeIgnoredWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            using System;
            using Fluentify.Internal;

            public static partial class TwoOfThreeIgnoredExtensions
            {
                internal static global::Fluentify.Classes.Testing.TwoOfThreeIgnored With(
                    this global::Fluentify.Classes.Testing.TwoOfThreeIgnored subject,
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

                    return new global::Fluentify.Classes.Testing.TwoOfThreeIgnored
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