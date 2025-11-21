namespace Fluentify;

using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

/// <summary>
/// Serves as a template for attribute generation.
/// </summary>
public abstract class AttributeGenerator
    : IIncrementalGenerator
{
    private readonly string _name;
    private readonly string[] _targets;

    /// <summary>
    /// Serves as a template for attribute generation.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <param name="targets">The AttributeTargets for the attribute.</param>
    private protected AttributeGenerator(string name, params string[] targets)
    {
        _name = name;
        _targets = targets;
    }

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(Generate);
    }

    private void Generate(IncrementalGeneratorPostInitializationContext context)
    {
        string source = $$"""
            namespace Fluentify
            {
                using System;

                [AttributeUsage({{GetTargets()}}, Inherited = false, AllowMultiple = false)]
                internal sealed class {{_name}}Attribute
                    : Attribute
                {
                }
            }
            """;

        var text = SourceText.From(source, Encoding.UTF8);

        context.AddSource($"{_name}Attribute.g.cs", text);
    }

    private string GetTargets()
    {
        IEnumerable<string> parameters = _targets.Select(target => $"AttributeTargets.{target}");

        return string.Join(" | ", parameters);
    }
}