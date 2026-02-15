namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string InvalidDescriptorContent = """
        namespace Fluentify.Classes.Testing
        {
            using System.Collections.Generic;

            [Fluentify]
            public sealed class InvalidDescriptor
            {
                [Descriptor(" ")]
                public int Age { get; set; }

                [Descriptor("With Name")]
                public string Name { get; set; }
        
                [Descriptor("&Invalid")]
                public IReadOnlyList<object> Attributes { get; set; }
            }
        }
        """;

    public static readonly Declared InvalidDescriptor;

    public static readonly Generated InvalidDescriptorWithAgeExtensions = new(
        InvalidDescriptorWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.InvalidDescriptorExtensions.WithAge");

    public static readonly Generated InvalidDescriptorWithAttributesExtensions = new(
        InvalidDescriptorWithAttributesExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.InvalidDescriptorExtensions.WithAttributes");

    public static readonly Generated InvalidDescriptorWithNameExtensions = new(
        InvalidDescriptorWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.InvalidDescriptorExtensions.WithName");

    public static readonly Generated InvalidDescriptorWithExtensions = new(
        InvalidDescriptorWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.InvalidDescriptorExtensions.With");

    private const string InvalidDescriptorWithAgeExtensionsContent = """
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

            public static partial class InvalidDescriptorExtensions
            {
                public static global::Fluentify.Classes.Testing.InvalidDescriptor WithAge(
                    this global::Fluentify.Classes.Testing.InvalidDescriptor subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.InvalidDescriptor
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

    private const string InvalidDescriptorWithAttributesExtensionsContent = """
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

            public static partial class InvalidDescriptorExtensions
            {
                public static global::Fluentify.Classes.Testing.InvalidDescriptor WithAttributes(
                    this global::Fluentify.Classes.Testing.InvalidDescriptor subject,
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

                    return new global::Fluentify.Classes.Testing.InvalidDescriptor
                    {
                        Age = subject.Age,
                        Attributes = value,
                        Name = subject.Name,
                    };
                }

                public static global::Fluentify.Classes.Testing.InvalidDescriptor WithAttributes(
                    this global::Fluentify.Classes.Testing.InvalidDescriptor subject,
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

    private const string InvalidDescriptorWithNameExtensionsContent = """
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

            public static partial class InvalidDescriptorExtensions
            {
                public static global::Fluentify.Classes.Testing.InvalidDescriptor WithName(
                    this global::Fluentify.Classes.Testing.InvalidDescriptor subject,
                    string value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.InvalidDescriptor
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

    private const string InvalidDescriptorWithExtensionsContent = """
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif
        
        #pragma warning disable CS8625

        namespace Fluentify.Classes.Testing
        {
            using System;
            using Fluentify.Internal;

            public static partial class InvalidDescriptorExtensions
            {
                internal static global::Fluentify.Classes.Testing.InvalidDescriptor With(
                    this global::Fluentify.Classes.Testing.InvalidDescriptor subject,
                    Func<int> age = default,
                    Func<global::System.Collections.Generic.IReadOnlyList<object>> attributes = default,
                    Func<string> name = default)
                {
                    subject.ThrowIfNull("subject");

                    var ageValue = ReferenceEquals(age, null) ? subject.Age : age();
                    var attributesValue = ReferenceEquals(attributes, null) ? subject.Attributes : attributes();
                    var nameValue = ReferenceEquals(name, null) ? subject.Name : name();

                    return new global::Fluentify.Classes.Testing.InvalidDescriptor
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