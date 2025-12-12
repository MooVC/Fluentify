namespace Fluentify.Console.Class;

[Fluentify]
public sealed class SkipAutoInstantiationOnType
{
    public int Age { get; set; }

    public Dependent Dependency { get; set; } = new Dependent();

    [SkipAutoInstantiation]
    public sealed class Dependent
    {
        public string Name { get; set; } = string.Empty;
    }
}