namespace Fluentify.Visitors;

using Fluentify.Model;
using Fluentify.Syntax;
using Graphify;

internal sealed class GenerateConstructorWhenSubjectVisited : ISubjectVisitor<Subject.Graph, Source>
{
    public IEnumerable<Source> Observe(Subject.Graph instance)
    {
        Subject subject = ((IGraph<Subject>)instance).Root;

        if (subject.IsRecord && subject.IsPartial && !subject.HasDefaultConstructor)
        {
            yield return new Source(subject.GetConstructor(), $"{subject.Name}.ctor");
        }
    }
}