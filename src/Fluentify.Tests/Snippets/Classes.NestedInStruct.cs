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

    public static readonly Generated NestedInStructWithExtensions = new(
        NestedInStructWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInStructExtensions.With");

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
                        Attributes = value,
                        Name = subject.Name,
                    };
                }

                public static global::Fluentify.Classes.Testing.Outter.NestedInStruct WithAttributes(
                    this global::Fluentify.Classes.Testing.Outter.NestedInStruct subject,
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

    private const string NestedInStructWithExtensionsContent = """
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif
        
        #pragma warning disable CS8625

        namespace Fluentify.Classes.Testing
        {
            using System;
            using Fluentify.Internal;

            public static partial class NestedInStructExtensions
            {
                internal static global::Fluentify.Classes.Testing.Outter.NestedInStruct With(
                    this global::Fluentify.Classes.Testing.Outter.NestedInStruct subject,
                    Func<int> age = default,
                    Func<global::System.Collections.Generic.IReadOnlyList<object>> attributes = default,
                    Func<string> name = default)
                {
                    subject.ThrowIfNull("subject");

                    var ageValue = ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = ReferenceEquals(name, null) ? subject.Name : name();

                    return new global::Fluentify.Classes.Testing.Outter.NestedInStruct
                    {
                        Age = ageValue,
                        Attributes = attributesValue,
                        Name = nameValue,
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