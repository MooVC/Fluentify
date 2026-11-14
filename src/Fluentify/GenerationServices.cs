namespace Fluentify;

using Fluentify.Model;
using Fluentify.Visitors;
using PropertyNode = Fluentify.Model.Subject.Graph.Properties.Property;
using Type = System.Type;

internal sealed class GenerationServices : IServiceProvider
{
    private static readonly Dictionary<Type, object> _visitors = new()
    {
        [typeof(IEnumerable<ISubjectVisitor<Subject.Graph, Source>>)] = new ISubjectVisitor<Subject.Graph, Source>[]
        {
            new GenerateConstructorWhenSubjectVisited(),
            new GenerateWithExtensionsWhenSubjectVisited(),
        },
        [typeof(IEnumerable<ISubjectVisitor<PropertyNode, Source>>)] = new ISubjectVisitor<PropertyNode, Source>[]
        {
            new GenerateExtensionsWhenPropertyVisited(),
        },
    };

    public static IServiceProvider Instance { get; } = new GenerationServices();

    public object? GetService(Type serviceType)
    {
        _ = _visitors.TryGetValue(serviceType, out object? visitors);
        return visitors;
    }
}