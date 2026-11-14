namespace Fluentify.Syntax;

using Fluentify.Model;
using MooVC.Syntax;
using MooVC.Syntax.CSharp;
using Property = Fluentify.Model.Property;

/// <summary>
/// Provides extensions relating to <see cref="Subject"/>.
/// </summary>
internal static partial class SubjectExtensions
{
    /// <summary>
    /// Gets the source code needed to add the With extension to the <paramref name="subject"/>.
    /// </summary>
    /// <param name="subject">The <see cref="Subject"/> to which the With extension is to be added.</param>
    /// <returns>
    /// The source code needed to add the With extension to the <paramref name="subject"/>.
    /// </returns>
    public static Snippet GetWithExtensions(this Subject subject)
    {
        Symbol type = subject.Type.ToSymbol(subject.SupportsNullableReferenceTypes);
        Symbol function = (Name: "Func", Qualifier: "System");
        var properties = subject.Properties
            .OrderBy(property => property.Name)
            .Select(property => (Property: property, Name: ToParameterName(property.Name)))
            .ToArray();

        Parameter receiver = Parameter.Undefined.Named("Subject")
            .OfType(type)
            .WithModifier(Parameter.Modes.This);
        var parameters = properties.Select(property => Parameter.Undefined.Named(property.Name)
            .OfType(function.WithArguments(property.Property.Kind.Type.ToSymbol(subject.SupportsNullableReferenceTypes)))
            .DefaultedTo("default"));

        Method method = Method.Undefined
            .Named(subject.GetExtensionDeclaration("With"))
            .WithExtensibility(Modifiers.Static)
            .WithScope(Scopes.Internal)
            .Accepts([receiver, .. parameters])
            .Returns(Result.Void.OfType(type))
            .WithBody(BuildWithBody(type, properties));

        return subject.ToExtensionSource(method);
    }

    private static Snippet BuildWithBody(Symbol type, (Property Property, string Name)[] properties)
    {
        IEnumerable<string> assignments = properties.Select(property =>
            $"var {property.Name}Value = global::System.Object.ReferenceEquals({property.Name}, null) ? subject.{property.Property.Name} : {property.Name}();");
        IEnumerable<string> checks = properties
            .Select(property => property.Property.GetValueNullCheck($"{property.Name}Value", property.Name.TrimStart('@')))
            .Where(check => !string.IsNullOrEmpty(check));
        IEnumerable<string> initializers = properties.Select(property => $"{property.Property.Name} = {property.Name}Value,");

        Snippet guard = Snippet.From(Configuration.Options, PropertyExtensions.GetParameterNullCheck("subject", "subject"));
        Snippet result = Snippet.From(Configuration.Options, initializers.ToArray())
            .Block(Configuration.Options, $"return new {type.ToSnippet(Configuration.Options)}")
            .Append(';');

        return Snippet.Blank.Combine(
            Configuration.Options,
            guard,
            Snippet.From(Configuration.Options, assignments.ToArray()),
            Snippet.From(Configuration.Options, checks.ToArray()),
            result);
    }

    private static string ToParameterName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return name;
        }

        name = string.Concat(char.ToLowerInvariant(name[0]), name.Substring(1));

        if (string.Equals(name, "subject", StringComparison.Ordinal))
        {
            return "subject1";
        }

        return name.IsReserved() ? string.Concat("@", name) : name;
    }
}