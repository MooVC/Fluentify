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
        using System;
        using System.Collections.Generic;
        using System.Linq;
        using Fluentify.Internal;

        public static partial class GlobalExtensions
        {
            public static global::Global WithAge(
                this global::Global subject,
                int value)
            {
                subject.ThrowIfNull("subject");

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
        using System;
        using System.Collections.Generic;
        using System.Linq;
        using Fluentify.Internal;

        public static partial class GlobalExtensions
        {
            public static global::Global WithAttributes(
                this global::Global subject,
                Func<object, object> builder,
                params object[] values)
            {
                subject.ThrowIfNull("subject");
                builder.ThrowIfNull("builder");
                values.ThrowIfNull("values");

                foreach (var value in values)
                {
                    subject = subject.WithAttributes(value, builder);
                }

                return subject;
            }

            public static global::Global WithAttributes(
                this global::Global subject,
                object instance,
                Func<object, object> builder)
            {
                subject.ThrowIfNull("subject");
                builder.ThrowIfNull("builder");

                instance = builder(instance);

                return subject.WithAttributes(instance);
            }

            public static global::Global WithAttributes(
                this global::Global subject,
                Func<object, object> builder)
            {
                subject.ThrowIfNull("subject");
                builder.ThrowIfNull("builder");

                var instance = new object();

                instance = builder(instance);

                return subject.WithAttributes(instance);
            }

            public static global::Global WithAttributes(
                this global::Global subject,
                params object[] values)
            {
                subject.ThrowIfNull("subject");
                values.ThrowIfNull("values");

                global::System.Collections.Generic.IReadOnlyList<object> value = values;

                if (subject.Attributes != null)
                {
                    value = subject.Attributes
                        .Union(values)
                        .ToArray();
                }

                value.ThrowIfNull("value");

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
        using System;
        using System.Collections.Generic;
        using System.Linq;
        using Fluentify.Internal;

        public static partial class GlobalExtensions
        {
            public static global::Global WithName(
                this global::Global subject,
                string value)
            {
                subject.ThrowIfNull("subject");

                value.ThrowIfNull("value");

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
        using System;
        using Fluentify.Internal;

        public static partial class GlobalExtensions
        {
            internal static global::Global With(
                this global::Global subject,
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