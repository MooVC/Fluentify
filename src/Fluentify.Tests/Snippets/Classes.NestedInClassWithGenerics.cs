namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string NestedInClassWithGenericsContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            public sealed class Outter<TOutter>
                where TOutter : class
            {
                [Fluentify]
                public sealed class NestedInClassWithGenerics<TInner>
                    where TInner : struct
                {
                    public int Age { get; set; }

                    public string Name { get; set; }

                    public IReadOnlyList<object> Attributes { get; set; }
                }
            }
        }
        """;

    public static readonly Declared NestedInClassWithGenerics;

    public static readonly Generated NestedInClassWithGenericsWithAgeExtensions = new(
        NestedInClassWithGenericsWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInClassWithGenericsExtensions.WithAge");

    public static readonly Generated NestedInClassWithGenericsWithAttributesExtensions = new(
        NestedInClassWithGenericsWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInClassWithGenericsExtensions.WithAttributes");

    public static readonly Generated NestedInClassWithGenericsWithNameExtensions = new(
        NestedInClassWithGenericsWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInClassWithGenericsExtensions.WithName");

    public static readonly Generated NestedInClassWithGenericsWithExtensions = new(
        NestedInClassWithGenericsWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInClassWithGenericsExtensions.With");

    private const string NestedInClassWithGenericsWithAgeExtensionsContent = """
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

            public static partial class NestedInClassWithGenericsExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> WithAge<TOutter, TInner>(
                    this global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> subject,
                    int value)
                    where TOutter : class
                    where TInner : struct
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner>
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

    private const string NestedInClassWithGenericsWithAttributesExtensionsContent = """
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

            public static partial class NestedInClassWithGenericsExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> WithAttributes<TOutter, TInner>(
                    this global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> subject,
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

                    return new global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner>
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }

                public static global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> WithAttributes<TOutter, TInner>(
                    this global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> subject,
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

    private const string NestedInClassWithGenericsWithNameExtensionsContent = """
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

            public static partial class NestedInClassWithGenericsExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> WithName<TOutter, TInner>(
                    this global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> subject,
                    string value)
                    where TOutter : class
                    where TInner : struct
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner>
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

    private const string NestedInClassWithGenericsWithExtensionsContent = """
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif

        #pragma warning disable CS8625

        namespace Fluentify.Classes.Testing
        {
            using System;
            using Fluentify.Internal;

            public static partial class NestedInClassWithGenericsExtensions
            {
                internal static global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> With<TOutter, TInner>(
                    this global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner> subject,
                    Func<int> age = default,
                    Func<global::System.Collections.Generic.IReadOnlyList<object>> attributes = default,
                    Func<string> name = default)
                    where TOutter : class
                    where TInner : struct
                {
                    subject.ThrowIfNull("subject");

                    var ageValue = ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = ReferenceEquals(name, null) ? subject.Name : name();

                    return new global::Fluentify.Classes.Testing.Outter<TOutter>.NestedInClassWithGenerics<TInner>
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