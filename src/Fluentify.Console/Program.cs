namespace Fluentify.Console;

using Fluentify.Console.Class.SkipAutoInstantiation;
using static System.Console;

using SkipAuto = Fluentify.Console.Class.SkipAutoInstantiation;

/// <summary>
/// Entry point for the application, which is used to facilitate debugging of the Fluentify project.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Entry point for the application, which is used to facilitate debugging of the Fluentify project.
    /// </summary>
    public static void Main()
    {
        WriteLine("Test Application for Fluentify");

        var example = new SkipAuto.Example
        {
            Dependency = new SkipAuto.Example.DependencySettings
            {
                ConnectionString = "Server=Demo;",
            },
            Name = "Skip Auto Instantiation",
        };

        WriteLine(example.Dependency.ConnectionString);
    }
}