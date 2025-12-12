namespace Fluentify.Console.Class;

[Fluentify]
public sealed class SkipAutoInstantiationOnProperty
{
    public int Age { get; set; }

    [SkipAutoInstantiation]
    public Dependent Dependency { get; set; } = new Dependent();

    public sealed class Dependent
    {
        public string Name { get; set; } = string.Empty;
    }
}