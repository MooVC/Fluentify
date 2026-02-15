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
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif
        
        #pragma warning disable CS8625

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

        #pragma warning restore CS8625
        
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;

    private const string GlobalWithAttributesExtensionsContent = """
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif
        
        #pragma warning disable CS8625

        using System;
        using System.Collections.Generic;
        using System.Linq;
        using Fluentify.Internal;

        public static partial class GlobalExtensions
        {
            public static global::Global WithAttributes(
                this global::Global subject,
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

                return new global::Global
                {
                    Age = subject.Age,
                    Attributes = value,
                    Name = subject.Name,
                };
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
        }
        
        #pragma warning restore CS8625
        
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;

    private const string GlobalWithNameExtensionsContent = """
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif
        
        #pragma warning disable CS8625

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

                return new global::Global
                {
                    Age = subject.Age,
                    Attributes = subject.Attributes,
                    Name = value,
                };
            }
        }

        #pragma warning restore CS8625
        
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;

    private const string GlobalWithExtensionsContent = """
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif
        
        #pragma warning disable CS8625

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

                return new global::Global
                {
                    Age = ageValue,
                    Attributes = attributesValue,
                    Name = nameValue,
                };
            }
        }

        #pragma warning restore CS8625
        
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;
}