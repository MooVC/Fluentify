namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string GlobalContent = """
        using System.Collections.Generic;
        using Fluentify;

        [Fluentify]
        public sealed class Global
        {
            public int Age { get; set; }

            public string Name { get; set; }

            public IReadOnlyList<object> Attributes { get; set; }
        }
        """;

    public static readonly Declared Global;

    public static readonly Generated GlobalWithAgeExtensions = new(
        GlobalWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "GlobalExtensions.WithAge");

    public static readonly Generated GlobalWithAttributesExtensions = new(
        GlobalWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "GlobalExtensions.WithAttributes");

    public static readonly Generated GlobalWithNameExtensions = new(
        GlobalWithNameExtensionsContent,
        typeof(ClassGenerator),
        "GlobalExtensions.WithName");

    public static readonly Generated GlobalWithExtensions = new(
        GlobalWithExtensionsContent,
        typeof(ClassGenerator),
        "GlobalExtensions.With");

    private const string GlobalWithAgeExtensionsContent = """
        public static partial class GlobalExtensions
        {
            public static global::Global WithAge(this global::Global subject, int value)
            {
                global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                return new global::Global
                {
                    Age = value,
                    Attributes = subject.Attributes,
                    Name = subject.Name,
                };
            }
        }
        """;

    private const string GlobalWithAttributesExtensionsContent = """
        public static partial class GlobalExtensions
        {
            public static global::Global WithAttributes(this global::Global subject, global::System.Func<object, object> builder, params object[] values)
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

            public static global::Global WithAttributes(this global::Global subject, object instance, global::System.Func<object, object> builder)
            {
                global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                instance = builder(instance);

                return subject.WithAttributes(instance);
            }

            public static global::Global WithAttributes(this global::Global subject, global::System.Func<object, object> builder)
            {
                global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                global::Fluentify.Internal.Extensions.ThrowIfNull(builder, "builder");

                var instance = new object();

                instance = builder(instance);

                return subject.WithAttributes(instance);
            }

            public static global::Global WithAttributes(this global::Global subject, params object[] values)
            {
                global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");
                global::Fluentify.Internal.Extensions.ThrowIfNull(values, "values");

                global::System.Collections.Generic.IReadOnlyList<object> value = values;

                if (subject.Attributes != null)
                {
                    value = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Union(subject.Attributes, values));
                }

                global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                return new global::Global
                {
                    Age = subject.Age,
                    Attributes = value,
                    Name = subject.Name,
                };
            }
        }
        """;

    private const string GlobalWithNameExtensionsContent = """
        public static partial class GlobalExtensions
        {
            public static global::Global WithName(this global::Global subject, string value)
            {
                global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                return new global::Global
                {
                    Age = subject.Age,
                    Attributes = subject.Attributes,
                    Name = value,
                };
            }
        }
        """;

    private const string GlobalWithExtensionsContent = """
        public static partial class GlobalExtensions
        {
            internal static global::Global With(this global::Global subject, global::System.Func<int> age = default, global::System.Func<global::System.Collections.Generic.IReadOnlyList<object>> attributes = default, global::System.Func<string> name = default)
            {
                global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                var ageValue = global::System.Object.ReferenceEquals(age, null) ? subject.Age : age();
                var attributesValue = global::System.Object.ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                var nameValue = global::System.Object.ReferenceEquals(name, null) ? subject.Name : name();

                global::Fluentify.Internal.Extensions.ThrowIfNull(attributesValue, "attributes");
                global::Fluentify.Internal.Extensions.ThrowIfNull(nameValue, "name");

                return new global::Global
                {
                    Age = ageValue,
                    Attributes = attributesValue,
                    Name = nameValue,
                };
            }
        }
        """;
}