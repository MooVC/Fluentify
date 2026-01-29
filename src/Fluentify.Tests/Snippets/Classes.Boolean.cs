namespace Fluentify.Snippets;

public static partial class Classes
{
    public const string BooleanContent = """
        namespace Fluentify.Classes.Testing
        {
            [Fluentify]
            public sealed class Boolean
            {
                public int Age { get; set; }

                public bool IsRetired { get; set; }

                public string Name { get; set; }
            }
        }
        """;

    public static readonly Declared Boolean;

    public static readonly Generated BooleanWithAgeExtensions = new(
        BooleanWithAgeExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.BooleanExtensions.WithAge");

    public static readonly Generated BooleanWithNameExtensions = new(
        BooleanWithNameExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.BooleanExtensions.WithName");

    public static readonly Generated BooleanIsRetiredExtensions = new(
        BooleanIsRetiredExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.BooleanExtensions.IsRetired");

    public static readonly Generated BooleanWithExtensions = new(
        BooleanWithExtensionsContent,
        typeof(ClassGenerator),
        "Fluentify.Classes.Testing.BooleanExtensions.With");

    private const string BooleanWithAgeExtensionsContent = """
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

            public static partial class BooleanExtensions
            {
                public static global::Fluentify.Classes.Testing.Boolean WithAge(
                    this global::Fluentify.Classes.Testing.Boolean subject,
                    int value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.Boolean
                    {
                        Age = value,
                        IsRetired = subject.IsRetired,
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

    private const string BooleanWithNameExtensionsContent = """
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

            public static partial class BooleanExtensions
            {
                public static global::Fluentify.Classes.Testing.Boolean WithName(
                    this global::Fluentify.Classes.Testing.Boolean subject,
                    string value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.Boolean
                    {
                        Age = subject.Age,
                        IsRetired = subject.IsRetired,
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

    private const string BooleanIsRetiredExtensionsContent = """
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

            public static partial class BooleanExtensions
            {
                public static global::Fluentify.Classes.Testing.Boolean IsRetired(
                    this global::Fluentify.Classes.Testing.Boolean subject,
                    bool value)
                {
                    subject.ThrowIfNull("subject");

                    return new global::Fluentify.Classes.Testing.Boolean
                    {
                        Age = subject.Age,
                        IsRetired = value,
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

    private const string BooleanWithExtensionsContent = """
        #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        #nullable enable
        #endif
        
        #pragma warning disable CS8625

        namespace Fluentify.Classes.Testing
        {
            using System;
            using Fluentify.Internal;

            public static partial class BooleanExtensions
            {
                internal static global::Fluentify.Classes.Testing.Boolean With(
                    this global::Fluentify.Classes.Testing.Boolean subject,
                    Func<int> age = default,
                    Func<bool> isRetired = default,
                    Func<string> name = default)
                {
                    subject.ThrowIfNull("subject");

                    var ageValue = ReferenceEquals(age, null) ? subject.Age : age();
                    var isRetiredValue = ReferenceEquals(isRetired, null) ? subject.IsRetired : isRetired();
                    var nameValue = ReferenceEquals(name, null) ? subject.Name : name();

                    return new global::Fluentify.Classes.Testing.Boolean
                    {
                        Age = ageValue,
                        IsRetired = isRetiredValue,
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