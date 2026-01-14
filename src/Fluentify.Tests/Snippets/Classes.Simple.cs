namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string SimpleContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class Simple
            {
                public int Age { get; set; }

                public string Name { get; set; }

                public IReadOnlyList<object> Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared Simple;

    public static readonly Generated SimpleWithAgeExtensions = new(
        SimpleWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SimpleExtensions.WithAge");

    public static readonly Generated SimpleWithAttributesExtensions = new(
        SimpleWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SimpleExtensions.WithAttributes");

    public static readonly Generated SimpleWithNameExtensions = new(
        SimpleWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SimpleExtensions.WithName");

    private const string SimpleWithAgeExtensionsContent = """
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif
        
        #pragma warning disable CS8625

        namespace Fluentify.Classes.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SimpleExtensions
            {
                public static global::Fluentify.Classes.Testing.Simple WithAge(
                    this global::Fluentify.Classes.Testing.Simple subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.Simple
                    {
                        Age = value,
                        Attributes = subject.Attributes,
                        Name = subject.Name,
                    };
                }
            }
        }

        #pragma warning restore CS8625
        
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;

    private const string SimpleWithAttributesExtensionsContent = """
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif
        
        #pragma warning disable CS8625

        namespace Fluentify.Classes.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SimpleExtensions
            {
                public static global::Fluentify.Classes.Testing.Simple WithAttributes(
                    this global::Fluentify.Classes.Testing.Simple subject,
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

                    return new global::Fluentify.Classes.Testing.Simple
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }

                public static global::Fluentify.Classes.Testing.Simple WithAttributes(
                    this global::Fluentify.Classes.Testing.Simple subject,
                    Func<object, object> builder)
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    var instance = new object();

                    instance = builder(instance);

                    return subject.WithAttributes(instance);
                }
            }
        }

        #pragma warning restore CS8625
        
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;

    private const string SimpleWithNameExtensionsContent = """
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif
        
        #pragma warning disable CS8625

        namespace Fluentify.Classes.Testing
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Fluentify.Internal;

            public static partial class SimpleExtensions
            {
                public static global::Fluentify.Classes.Testing.Simple WithName(
                    this global::Fluentify.Classes.Testing.Simple subject,
                    string value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.Simple
                    {
                        Age = subject.Age,
                        Attributes = subject.Attributes,
                        Name = value,
                    };
                }
            }
        }

        #pragma warning restore CS8625
        
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;
}