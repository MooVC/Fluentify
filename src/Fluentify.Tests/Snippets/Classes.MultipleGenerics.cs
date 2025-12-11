namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string MultipleGenericsContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class MultipleGenerics<T1, T2, T3>
                where T1 : struct
                where T2 : class, new()
                where T3 : IEnumerable<string>
            {
                public T1 Age { get; set; }

                public T2 Name { get; set; }

                public T3 Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared MultipleGenerics;

    public static readonly Generated MultipleGenericsWithAgeExtensions = new(
        MultipleGenericsWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.MultipleGenericsExtensions.WithAge");

    public static readonly Generated MultipleGenericsWithAttributesExtensions = new(
        MultipleGenericsWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.MultipleGenericsExtensions.WithAttributes");

    public static readonly Generated MultipleGenericsWithNameExtensions = new(
        MultipleGenericsWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.MultipleGenericsExtensions.WithName");

    private const string MultipleGenericsWithAgeExtensionsContent = """
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

            public static partial class MultipleGenericsExtensions
            {
                public static global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> WithAge<T1, T2, T3>(
                    this global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> subject,
                    Func<T1, T1> builder)
                    where T1 : struct
                    where T2 : class, new()
                    where T3 : global::System.Collections.Generic.IEnumerable<string>
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    var instance = subject.Age;

                    instance = builder(instance);

                    return subject.WithAge(instance);
                }

                public static global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> WithAge<T1, T2, T3>(
                    this global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> subject,
                    T1 value)
                    where T1 : struct
                    where T2 : class, new()
                    where T3 : global::System.Collections.Generic.IEnumerable<string>
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3>
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

    private const string MultipleGenericsWithAttributesExtensionsContent = """
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
        
            public static partial class MultipleGenericsExtensions
            {
                public static global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> WithAttributes<T1, T2, T3>(
                    this global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> subject,
                    Func<T3, T3> builder)
                    where T1 : struct
                    where T2 : class, new()
                    where T3 : global::System.Collections.Generic.IEnumerable<string>
                {
                    subject.ThrowIfNull("subject");
        
                    builder.ThrowIfNull("builder");
        
                    var instance = subject.Attributes;
        
                    if (instance is null)
                    {
                        throw new NotSupportedException();
                    }
        
                    instance = builder(instance);
        
                    return subject.WithAttributes(instance);
                }
        
                public static global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> WithAttributes<T1, T2, T3>(
                    this global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> subject,
                    T3 value)
                    where T1 : struct
                    where T2 : class, new()
                    where T3 : global::System.Collections.Generic.IEnumerable<string>
                {
                    subject.ThrowIfNull("subject");
        
                    return new global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3>
                    {
                        Age = subject.Age,
                        Attributes = value,
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

    private const string MultipleGenericsWithNameExtensionsContent = """
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

            public static partial class MultipleGenericsExtensions
            {
                public static global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> WithName<T1, T2, T3>(
                    this global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> subject,
                    Func<T2, T2> builder)
                    where T1 : struct
                    where T2 : class, new()
                    where T3 : global::System.Collections.Generic.IEnumerable<string>
                {
                    subject.ThrowIfNull("subject");

                    builder.ThrowIfNull("builder");

                    var instance = subject.Name;

                    if (instance is null)
                    {
                        instance = new T2();
                    }

                    instance = builder(instance);

                    return subject.WithName(instance);
                }

                public static global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> WithName<T1, T2, T3>(
                    this global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3> subject,
                    T2 value)
                    where T1 : struct
                    where T2 : class, new()
                    where T3 : global::System.Collections.Generic.IEnumerable<string>
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.MultipleGenerics<T1, T2, T3>
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