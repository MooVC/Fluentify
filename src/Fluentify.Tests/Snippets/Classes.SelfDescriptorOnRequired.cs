namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string SelfDescriptorOnRequiredContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class SelfDescriptorOnRequired
            {
                [Descriptor]
                public int Age { get; set; }
        
                [Descriptor("")]
                public string Name { get; set; }

                public IReadOnlyList<object> Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared SelfDescriptorOnRequired;

    public static readonly Generated SelfDescriptorOnRequiredAgeExtensions = new(
        SelfDescriptorOnRequiredAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SelfDescriptorOnRequiredExtensions.Age");

    public static readonly Generated SelfDescriptorOnRequiredWithAttributesExtensions = new(
        SelfDescriptorOnRequiredWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SelfDescriptorOnRequiredExtensions.WithAttributes");

    public static readonly Generated SelfDescriptorOnRequiredWithExtensions = new(
        SelfDescriptorOnRequiredWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SelfDescriptorOnRequiredExtensions.With");

    public static readonly Generated SelfDescriptorOnRequiredNameExtensions = new(
        SelfDescriptorOnRequiredNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.SelfDescriptorOnRequiredExtensions.Name");

    private const string SelfDescriptorOnRequiredAgeExtensionsContent = """
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

            public static partial class SelfDescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Classes.Testing.SelfDescriptorOnRequired Age(
                    this global::Fluentify.Classes.Testing.SelfDescriptorOnRequired subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SelfDescriptorOnRequired
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

    private const string SelfDescriptorOnRequiredWithAttributesExtensionsContent = """
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

            public static partial class SelfDescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Classes.Testing.SelfDescriptorOnRequired WithAttributes(
                    this global::Fluentify.Classes.Testing.SelfDescriptorOnRequired subject,
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

                    return new global::Fluentify.Classes.Testing.SelfDescriptorOnRequired
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }

                public static global::Fluentify.Classes.Testing.SelfDescriptorOnRequired WithAttributes(
                    this global::Fluentify.Classes.Testing.SelfDescriptorOnRequired subject,
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

    private const string SelfDescriptorOnRequiredNameExtensionsContent = """
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

            public static partial class SelfDescriptorOnRequiredExtensions
            {
                public static global::Fluentify.Classes.Testing.SelfDescriptorOnRequired Name(
                    this global::Fluentify.Classes.Testing.SelfDescriptorOnRequired subject,
                    string value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.SelfDescriptorOnRequired
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

    private const string SelfDescriptorOnRequiredWithExtensionsContent = """
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif
        
        #pragma warning disable CS8625

        namespace Fluentify.Classes.Testing
        {
            using System;
            using Fluentify.Internal;

            public static partial class SelfDescriptorOnRequiredExtensions
            {
                internal static global::Fluentify.Classes.Testing.SelfDescriptorOnRequired With(
                    this global::Fluentify.Classes.Testing.SelfDescriptorOnRequired subject,
                    Func<int> age = default,
                    Func<global::System.Collections.Generic.IReadOnlyList<object>> attributes = default,
                    Func<string> name = default)
                {
                    subject.ThrowIfNull("subject");

                    var ageValue = ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = ReferenceEquals(name, null) ? subject.Name : name();

                    return new global::Fluentify.Classes.Testing.SelfDescriptorOnRequired
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