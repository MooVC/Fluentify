namespace Fluentify.Console.Record;

[Fluentify]
public sealed partial record SkipAutoInstantiationOnProperty(
    int Age,
    [SkipAutoInstantiation] SkipAutoInstantiationOnProperty.Dependent Dependency)
{
    public sealed class Dependent
    {
        public string Name { get; set; } = string.Empty;
    }
}