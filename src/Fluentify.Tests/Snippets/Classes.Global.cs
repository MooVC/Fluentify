namespace Fluentify.Snippets;

public static partial class Classes
{
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

    private const string GlobalContent = """
        using System.Collections.Generic;
        using Fluentify;

        [Fluentify]
        public sealed partial class Global
        {
            public int Age { get; set; }

            public string Name { get; set; }

            public IReadOnlyList<object> Attributes { get; set; }
        }
        """;

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
                    Name = subject.Name,
                    Attributes = subject.Attributes,
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
                    Name = subject.Name,
                    Attributes = value,
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
                    Name = value,
                    Attributes = subject.Attributes,
                };
            }
        }

        #pragma warning restore CS8625
        
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;
}