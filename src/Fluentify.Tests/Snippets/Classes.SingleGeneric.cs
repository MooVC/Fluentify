namespace Fluentify.Snippets;

public static partial class Classes
{
    public static readonly Declared SingleGeneric;

    public static readonly Generated SingleGenericWithAgeExtensions = new(
        SingleGenericWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SingleGenericExtensions.WithAge");

    public static readonly Generated SingleGenericWithAttributesExtensions = new(
        SingleGenericWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SingleGenericExtensions.WithAttributes");

    public static readonly Generated SingleGenericWithNameExtensions = new(
        SingleGenericWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SingleGenericExtensions.WithName");

    private const string SingleGenericContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections;

            [Fluentify]
            public sealed class SingleGeneric<T>
                where T : IEnumerable
            {
                public int Age { get; set; }

                public string Name { get; set; }

                public T Attributes { get; set; }
            }
        }
        """;

    private const string SingleGenericWithAgeExtensionsContent = """
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

            public static partial class SingleGenericExtensions
            {
                public static global::Fluentify.Classes.Testing.SingleGeneric<T> WithAge<T>(
                    this global::Fluentify.Classes.Testing.SingleGeneric<T> subject,
                    int value)
                    where T : global::System.Collections.IEnumerable
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SingleGeneric<T>
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

    private const string SingleGenericWithAttributesExtensionsContent = """
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

            public static partial class SingleGenericExtensions
            {
                public static global::Fluentify.Classes.Testing.SingleGeneric<T> WithAttributes<T>(
                    this global::Fluentify.Classes.Testing.SingleGeneric<T> subject,
                    T value)
                    where T : global::System.Collections.IEnumerable
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SingleGeneric<T>
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

    private const string SingleGenericWithNameExtensionsContent = """
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

            public static partial class SingleGenericExtensions
            {
                public static global::Fluentify.Classes.Testing.SingleGeneric<T> WithName<T>(
                    this global::Fluentify.Classes.Testing.SingleGeneric<T> subject,
                    string value)
                    where T : global::System.Collections.IEnumerable
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SingleGeneric<T>
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