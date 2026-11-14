namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string OneOfThreeIgnoredContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class OneOfThreeIgnored
            {
                public int Age { get; set; }

                [Ignore]
                public string Name { get; set; }

                public IReadOnlyList<object> Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared OneOfThreeIgnored;

    public static readonly Generated OneOfThreeIgnoredWithAgeExtensions = new(
        OneOfThreeIgnoredWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.OneOfThreeIgnoredExtensions.WithAge");

    public static readonly Generated OneOfThreeIgnoredWithAttributesExtensions = new(
        OneOfThreeIgnoredWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.OneOfThreeIgnoredExtensions.WithAttributes");

    public static readonly Generated OneOfThreeIgnoredWithExtensions = new(
        OneOfThreeIgnoredWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.OneOfThreeIgnoredExtensions.With");

    private const string OneOfThreeIgnoredWithAgeExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class OneOfThreeIgnoredExtensions
            {
                public static global::Fluentify.Classes.Testing.OneOfThreeIgnored WithAge(this global::Fluentify.Classes.Testing.OneOfThreeIgnored subject, int value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    return new global::Fluentify.Classes.Testing.OneOfThreeIgnored
                    {
                        Age = value,
                        Attributes = subject.Attributes,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string OneOfThreeIgnoredWithAttributesExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class OneOfThreeIgnoredExtensions
            {
                public static global::Fluentify.Classes.Testing.OneOfThreeIgnored WithAttributes(this global::Fluentify.Classes.Testing.OneOfThreeIgnored subject, global::System.Func<object, object> builder, params object[] values)
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

                public static global::Fluentify.Classes.Testing.OneOfThreeIgnored WithAttributes(this global::Fluentify.Classes.Testing.OneOfThreeIgnored subject, object instance, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.OneOfThreeIgnored WithAttributes(this global::Fluentify.Classes.Testing.OneOfThreeIgnored subject, global::System.Func<object, object> builder)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }

                public static global::Fluentify.Classes.Testing.OneOfThreeIgnored WithAttributes(this global::Fluentify.Classes.Testing.OneOfThreeIgnored subject, params object[] values)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                    global::System.Collections.Generic.IReadOnlyList<object> value = values;

                    if (subject.Attributes != null)
                    {
                        value = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Union(subject.Attributes, values));
                    }

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.OneOfThreeIgnored
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string OneOfThreeIgnoredWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class OneOfThreeIgnoredExtensions
            {
                internal static global::Fluentify.Classes.Testing.OneOfThreeIgnored With(this global::Fluentify.Classes.Testing.OneOfThreeIgnored subject, global::System.Func<int> age = default, global::System.Func<global::System.Collections.Generic.IReadOnlyList<object>> attributes = default, global::System.Func<string> name = default)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    var ageValue = global::System.Object.ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = global::System.Object.ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = global::System.Object.ReferenceEquals(name, null) ? subject.Name : name();

                    global::Fluentify.Internal.Extensions.ThrowIfNull(attributesValue, "attributes");
                    global::Fluentify.Internal.Extensions.ThrowIfNull(nameValue, "name");

                    return new global::Fluentify.Classes.Testing.OneOfThreeIgnored
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