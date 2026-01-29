namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string SelfDescriptorOnIgnoredContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class SelfDescriptorOnIgnored
            {
                public int Age { get; set; }

                [Descriptor, Ignore]
                public string Name { get; set; }

                public IReadOnlyList<object> Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared SelfDescriptorOnIgnored;

    public static readonly Generated SelfDescriptorOnIgnoredWithAgeExtensions = new(
        SelfDescriptorOnIgnoredWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SelfDescriptorOnIgnoredExtensions.WithAge");

    public static readonly Generated SelfDescriptorOnIgnoredWithAttributesExtensions = new(
        SelfDescriptorOnIgnoredWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SelfDescriptorOnIgnoredExtensions.WithAttributes");

    public static readonly Generated SelfDescriptorOnIgnoredWithExtensions = new(
        SelfDescriptorOnIgnoredWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SelfDescriptorOnIgnoredExtensions.With");

    private const string SelfDescriptorOnIgnoredWithAgeExtensionsContent = """
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

            public static partial class SelfDescriptorOnIgnoredExtensions
            {
                public static global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored WithAge(
                    this global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored
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

    private const string SelfDescriptorOnIgnoredWithAttributesExtensionsContent = """
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

            public static partial class SelfDescriptorOnIgnoredExtensions
            {
                public static global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored WithAttributes(
                    this global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored subject,
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

                    return new global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }

                public static global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored WithAttributes(
                    this global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored subject,
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

    private const string SelfDescriptorOnIgnoredWithExtensionsContent = """
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif
        
        #pragma warning disable CS8625

        namespace Fluentify.Classes.Testing
        {
            using System;
            using Fluentify.Internal;

            public static partial class SelfDescriptorOnIgnoredExtensions
            {
                internal static global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored With(
                    this global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored subject,
                    Func<int> age = default,
                    Func<global::System.Collections.Generic.IReadOnlyList<object>> attributes = default,
                    Func<string> name = default)
                {
                    subject.ThrowIfNull("subject");

                    var ageValue = ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = ReferenceEquals(name, null) ? subject.Name : name();

                    return new global::Fluentify.Classes.Testing.SelfDescriptorOnIgnored
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