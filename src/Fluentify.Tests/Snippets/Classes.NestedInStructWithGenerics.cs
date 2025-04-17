namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string NestedInStructWithGenericsContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            public struct Outter<TOutter>
                where TOutter : class
            {
                [Fluentify]
                public sealed class NestedInStructWithGenerics<TInner>
                    where TInner : struct
                {
                    public int Age { get; set; }

                    public string Name { get; set; }

                    public IReadOnlyList<object> Attributes { get; set; }
                }
            }
        }
        """;

    public static readonly Declared NestedInStructWithGenerics;

    public static readonly Generated NestedInStructWithGenericsWithAgeExtensions = new(
        NestedInStructWithGenericsWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInStructWithGenericsExtensions.WithAge");

    public static readonly Generated NestedInStructWithGenericsWithAttributesExtensions = new(
        NestedInStructWithGenericsWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInStructWithGenericsExtensions.WithAttributes");

    public static readonly Generated NestedInStructWithGenericsWithNameExtensions = new(
        NestedInStructWithGenericsWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInStructWithGenericsExtensions.WithName");

    private const string NestedInStructWithGenericsWithAgeExtensionsContent = """
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

            public static partial class NestedInStructWithGenericsExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> WithAge<TOutter, TInner>(
                    this global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject,
                    int value)
                    where TOutter : class
                    where TInner : struct
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner>
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

    private const string NestedInStructWithGenericsWithAttributesExtensionsContent = """
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

            public static partial class NestedInStructWithGenericsExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> WithAttributes<TOutter, TInner>(
                    this global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject,
                    params object[] values)
                    where TOutter : class
                    where TInner : struct
                {
                    subject.ThrowIfNull("subject");

                    global::System.Collections.Generic.IReadOnlyList<object> value = values;

                    if (subject.Attributes != null)
                    {
                        value = subject.Attributes
                            .Union(values)
                            .ToArray();
                    }

                    return new global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner>
                    {
                        Age = subject.Age,
                        Name = subject.Name,
                        Attributes = value,
                    };
                }

                public static global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> WithAttributes<TOutter, TInner>(
                    this global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject,
                    Func<object, object> builder)
                    where TOutter : class
                    where TInner : struct
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

    private const string NestedInStructWithGenericsWithNameExtensionsContent = """
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

            public static partial class NestedInStructWithGenericsExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> WithName<TOutter, TInner>(
                    this global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner> subject,
                    string value)
                    where TOutter : class
                    where TInner : struct
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInStructWithGenerics<TInner>
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