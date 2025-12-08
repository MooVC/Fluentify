namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string NestedInStructContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            public struct Outter
            {
                [Fluentify]
                public sealed class NestedInStruct
                {
                    public int Age { get; set; }

                    public string Name { get; set; }

                    public IReadOnlyList<object> Attributes { get; set; }
                }
            }
        }
        """;

    public static readonly Declared NestedInStruct;

    public static readonly Generated NestedInStructWithAgeExtensions = new(
        NestedInStructWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInStructExtensions.WithAge");

    public static readonly Generated NestedInStructWithAttributesExtensions = new(
        NestedInStructWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInStructExtensions.WithAttributes");

    public static readonly Generated NestedInStructWithNameExtensions = new(
        NestedInStructWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInStructExtensions.WithName");

    private const string NestedInStructWithAgeExtensionsContent = """
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

            public static partial class NestedInStructExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter.NestedInStruct WithAge(
                    this global::Fluentify.Classes.Testing.Outter.NestedInStruct subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.Outter.NestedInStruct
                    {
                        Age = value,
                        Name = subject.Name,
                        Attributes = subject.Attributes,
                    };
                }
            }
        }

        #pragma warning restore CS8625
        
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;

    private const string NestedInStructWithAttributesExtensionsContent = """
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

            public static partial class NestedInStructExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter.NestedInStruct WithAttributes(
                    this global::Fluentify.Classes.Testing.Outter.NestedInStruct subject,
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

                    return new global::Fluentify.Classes.Testing.Outter.NestedInStruct
                    {
                        Age = subject.Age,
                        Name = subject.Name,
                        Attributes = value,
                    };
                }

            }
        }

        #pragma warning restore CS8625
        
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable restore
        #endif
        """;

    private const string NestedInStructWithNameExtensionsContent = """
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

            public static partial class NestedInStructExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter.NestedInStruct WithName(
                    this global::Fluentify.Classes.Testing.Outter.NestedInStruct subject,
                    string value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.Outter.NestedInStruct
                    {
                        Age = subject.Age,
                        Name = value,
                        Attributes = subject.Attributes,
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