namespace Fluentify.Console.Record;

[Fluentify]
public sealed partial record SkipAutoInstantiationOnType(
    int Age,
    SkipAutoInstantiationOnType.Dependent Dependency)
{
    [SkipAutoInstantiation]
    public sealed class Dependent
    {
        public string Name { get; set; } = string.Empty;
    }
}