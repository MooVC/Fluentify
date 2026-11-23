namespace Fluentify.Visitors;

using Fluentify.Model;
using Fluentify.Syntax;
using MooVC.Syntax;
using MooVC.Syntax.CSharp;
using Property = Fluentify.Model.Property;
using PropertyNode = Fluentify.Model.Subject.Graph.Properties.Property;

internal sealed class GenerateExtensionsWhenPropertyVisited : ISubjectVisitor<PropertyNode, Source>
{
    public IEnumerable<Source> Observe(PropertyNode instance)
    {
        Subject subject = instance._Root;
        Property property = instance._Value;

        if (property.IsIgnored || (!subject.IsRecord && !subject.HasDefaultConstructor))
        {
            yield break;
        }

        Snippet source = property.GetExtensions(subject, property => GetScalarBody(property, subject));

        if (!source.IsEmpty)
        {
            yield return new Source(source, $"{subject.Name}Extensions.{property.Descriptor}");
        }
    }

    private static string GetScalarBody(Property property, Subject subject)
    {
        Snippet body = subject.IsRecord
            ? Snippet.From(Configuration.Options, $"{property.Name} = value,").Block(Configuration.Options, "return subject with").Append(';')
            : GetClassInitializer(property, subject);

        return property.AddValueNullCheck(body, "value", "value");
    }

    private static Snippet GetClassInitializer(Property property, Subject subject)
    {
        string[] initializers = subject.Properties
            .OrderBy(member => member.Name)
            .Select(member => member == property ? $"{member.Name} = value," : $"{member.Name} = subject.{member.Name},")
            .ToArray();
        Symbol type = subject.Type.ToSymbol(subject.SupportsNullableReferenceTypes);

        return Snippet.From(Configuration.Options, initializers)
            .Block(Configuration.Options, $"return new {type.ToSnippet(Configuration.Options)}")
            .Append(';');
    }
}