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
}