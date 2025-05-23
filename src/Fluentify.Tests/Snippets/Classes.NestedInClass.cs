﻿namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string NestedInClassContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            public sealed class Outter
            {
                [Fluentify]
                public sealed class NestedInClass
                {
                    public int Age { get; set; }

                    public string Name { get; set; }

                    public IReadOnlyList<object> Attributes { get; set; }
                }
            }
        }
        """;

    public static readonly Declared NestedInClass;

    public static readonly Generated NestedInClassWithAgeExtensions = new(
        NestedInClassWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInClassExtensions.WithAge");

    public static readonly Generated NestedInClassWithAttributesExtensions = new(
        NestedInClassWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInClassExtensions.WithAttributes");

    public static readonly Generated NestedInClassWithNameExtensions = new(
        NestedInClassWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.Outter.NestedInClassExtensions.WithName");

    private const string NestedInClassWithAgeExtensionsContent = """
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

            public static partial class NestedInClassExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter.NestedInClass WithAge(
                    this global::Fluentify.Classes.Testing.Outter.NestedInClass subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.Outter.NestedInClass
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

    private const string NestedInClassWithAttributesExtensionsContent = """
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

            public static partial class NestedInClassExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter.NestedInClass WithAttributes(
                    this global::Fluentify.Classes.Testing.Outter.NestedInClass subject,
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

                    return new global::Fluentify.Classes.Testing.Outter.NestedInClass
                    {
                        Age = subject.Age,
                        Name = subject.Name,
                        Attributes = value,
                    };
                }

                public static global::Fluentify.Classes.Testing.Outter.NestedInClass WithAttributes(
                    this global::Fluentify.Classes.Testing.Outter.NestedInClass subject,
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

    private const string NestedInClassWithNameExtensionsContent = """
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

            public static partial class NestedInClassExtensions
            {
                public static global::Fluentify.Classes.Testing.Outter.NestedInClass WithName(
                    this global::Fluentify.Classes.Testing.Outter.NestedInClass subject,
                    string value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.Outter.NestedInClass
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