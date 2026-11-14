namespace Fluentify;

using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using MooVC.Syntax;
using MooVC.Syntax.CSharp;
using Attribute = System.Attribute;

/// <summary>
/// Serves as a template for attribute generation.
/// </summary>
public abstract class AttributeGenerator
    : IIncrementalGenerator
{
    private readonly string _name;
    private readonly AttributeTargets[] _targets;

    /// <summary>
    /// Serves as a template for attribute generation.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <param name="targets">The AttributeTargets for the attribute.</param>
    private protected AttributeGenerator(string name, params AttributeTargets[] targets)
    {
        _name = name;
        _targets = targets;
    }

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(Generate);
    }

    internal static string CreateStringAttributeSource(string name, string property, string parameter, params AttributeTargets[] targets)
    {
        Class declaration = CreateDeclaration(name, targets)
            .WithConstructors(constructor => constructor.WithBody($"{property} = string.Empty;"))
            .WithConstructors(constructor => constructor
                .WithParameters(argument => argument.Named(parameter).OfType(typeof(string)))
                .WithBody($"{property} = {parameter};"))
            .WithProperties(member => member
                .Named(property)
                .OfType(typeof(string))
                .WithBehaviours(accessors => accessors.WithSet(setter => setter.WithMode(Property.Methods.Setter.Modes.ReadOnly))));

        return CreateSource(declaration);
    }

    private static Class CreateDeclaration(string name, AttributeTargets[] targets)
    {
        Symbol targetType = typeof(AttributeTargets);
        string qualifiedTarget = targetType.ToSnippet(Configuration.Options.Types);
        string targetExpression = string.Join(" | ", targets.Select(target => $"{qualifiedTarget}.{target}"));

        return Class.Undefined
            .Named($"{name}Attribute")
            .IsPartial(false)
            .WithExtensibility(Modifiers.Sealed)
            .WithScope(Scopes.Internal)
            .DerivesFrom(typeof(Attribute))
            .AttributedWith(usage => usage
                .Named(typeof(AttributeUsageAttribute))
                .WithArguments(
                    (Name: string.Empty, Value: targetExpression),
                    (Name: nameof(AttributeUsageAttribute.Inherited), Value: "false"),
                    (Name: nameof(AttributeUsageAttribute.AllowMultiple), Value: "false")));
    }

    private static string CreateSource(Class declaration)
    {
        return Definition.Undefined
            .From(typeof(AttributeGenerator))
            .For<Class>(_ => declaration)
            .ToSnippet(Configuration.Options);
    }

    private void Generate(IncrementalGeneratorPostInitializationContext context)
    {
        string source = CreateSource(CreateDeclaration(_name, _targets));
        var text = SourceText.From(source, Encoding.UTF8);

        context.AddSource($"{_name}Attribute.g.cs", text);
    }
}