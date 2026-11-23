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
        namespace Fluentify.Classes.Testing
        {
            public static partial class BooleanExtensions
            {
                public static global::Fluentify.Classes.Testing.Boolean WithAge(this global::Fluentify.Classes.Testing.Boolean subject, int value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    return new global::Fluentify.Classes.Testing.Boolean
                    {
                        Age = value,
                        IsRetired = subject.IsRetired,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string BooleanWithNameExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class BooleanExtensions
            {
                public static global::Fluentify.Classes.Testing.Boolean WithName(this global::Fluentify.Classes.Testing.Boolean subject, string value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    global::Fluentify.Internal.Extensions.ThrowIfNull(value, "value");

                    return new global::Fluentify.Classes.Testing.Boolean
                    {
                        Age = subject.Age,
                        IsRetired = subject.IsRetired,
                        Name = value,
                    };
                }
            }
        }
        """;

    private const string BooleanIsRetiredExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class BooleanExtensions
            {
                public static global::Fluentify.Classes.Testing.Boolean IsRetired(this global::Fluentify.Classes.Testing.Boolean subject, bool value)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    return new global::Fluentify.Classes.Testing.Boolean
                    {
                        Age = subject.Age,
                        IsRetired = value,
                        Name = subject.Name,
                    };
                }
            }
        }
        """;

    private const string BooleanWithExtensionsContent = """
        namespace Fluentify.Classes.Testing
        {
            public static partial class BooleanExtensions
            {
                internal static global::Fluentify.Classes.Testing.Boolean With(this global::Fluentify.Classes.Testing.Boolean subject, global::System.Func<int> age = default, global::System.Func<bool> isRetired = default, global::System.Func<string> name = default)
                {
                    global::Fluentify.Internal.Extensions.ThrowIfNull(subject, "subject");

                    var ageValue = global::System.Object.ReferenceEquals(age, null) ? subject.Age : age();
                    var isRetiredValue = global::System.Object.ReferenceEquals(isRetired, null) ? subject.IsRetired : isRetired();
                    var nameValue = global::System.Object.ReferenceEquals(name, null) ? subject.Name : name();

                    global::Fluentify.Internal.Extensions.ThrowIfNull(nameValue, "name");

                    return new global::Fluentify.Classes.Testing.Boolean
                    {
                        Age = ageValue,
                        IsRetired = isRetiredValue,
                        Name = nameValue,
                    };
                }
            }
        }
        """;
}