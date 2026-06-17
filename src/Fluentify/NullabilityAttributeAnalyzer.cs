namespace Fluentify;

using System.Collections.Immutable;
using Fluentify.Semantics;
using Fluentify.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using static Fluentify.NullabilityAttributeAnalyzer_Resources;

/// <summary>
/// Analyzes usage of nullability attributes that affect Fluentify null-assignment behavior.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class NullabilityAttributeAnalyzer
    : Analyzer<AttributeSyntax>
{
    private const string AllowNullAttribute = "AllowNull";
    private const string CodeAnalysisNamespace = "System.Diagnostics.CodeAnalysis";
    private const string DisallowNullAttribute = "DisallowNull";
    private const string MaybeNullAttribute = "MaybeNull";

    /// <summary>
    /// Facilitates construction of the analyzer.
    /// </summary>
    public NullabilityAttributeAnalyzer()
        : base(SyntaxKind.Attribute)
    {
    }

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(
        RedundantAllowNullRule,
        RedundantDisallowNullRule,
        RedundantMaybeNullRule);

    /// <summary>
    /// Gets the descriptor associated with the redundant AllowNull rule (FLTFY14).
    /// </summary>
    /// <value>
    /// The descriptor associated with the redundant AllowNull rule (FLTFY14).
    /// </value>
    internal static DiagnosticDescriptor RedundantAllowNullRule { get; } = new(
        "FLTFY14",
        GetResourceString(nameof(RedundantAllowNullRuleTitle)),
        GetResourceString(nameof(RedundantAllowNullRuleMessageFormat)),
        "Usage",
        DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: GetResourceString(nameof(RedundantAllowNullRuleDescription)),
        helpLinkUri: GetHelpLinkUri("FLTFY14"));

    /// <summary>
    /// Gets the descriptor associated with the redundant DisallowNull rule (FLTFY16).
    /// </summary>
    /// <value>
    /// The descriptor associated with the redundant DisallowNull rule (FLTFY16).
    /// </value>
    internal static DiagnosticDescriptor RedundantDisallowNullRule { get; } = new(
        "FLTFY16",
        GetResourceString(nameof(RedundantDisallowNullRuleTitle)),
        GetResourceString(nameof(RedundantDisallowNullRuleMessageFormat)),
        "Usage",
        DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: GetResourceString(nameof(RedundantDisallowNullRuleDescription)),
        helpLinkUri: GetHelpLinkUri("FLTFY16"));

    /// <summary>
    /// Gets the descriptor associated with the redundant MaybeNull rule (FLTFY15).
    /// </summary>
    /// <value>
    /// The descriptor associated with the redundant MaybeNull rule (FLTFY15).
    /// </value>
    internal static DiagnosticDescriptor RedundantMaybeNullRule { get; } = new(
        "FLTFY15",
        GetResourceString(nameof(RedundantMaybeNullRuleTitle)),
        GetResourceString(nameof(RedundantMaybeNullRuleMessageFormat)),
        "Usage",
        DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: GetResourceString(nameof(RedundantMaybeNullRuleDescription)),
        helpLinkUri: GetHelpLinkUri("FLTFY15"));

    /// <inheritdoc/>
    protected override void AnalyzeNode(SyntaxNodeAnalysisContext context, AttributeSyntax syntax)
    {
        IMethodSymbol? symbol = GetSymbol(context, syntax);

        if (symbol?.ContainingType is not INamedTypeSymbol attribute || !TryGetAttributeName(attribute, out string attributeName))
        {
            return;
        }

        IPropertySymbol? property = GetProperty(context, syntax);

        if (property is null || !property.ContainingType.HasFluentify())
        {
            return;
        }

        Location location = syntax.GetLocation();

        if (attributeName == AllowNullAttribute && property.Type.IsNullable())
        {
            Raise(context, RedundantAllowNullRule, location, property.Name);
        }
        else if (attributeName == DisallowNullAttribute && !property.Type.IsNullable())
        {
            Raise(context, RedundantDisallowNullRule, location, property.Name);
        }
        else if (attributeName == MaybeNullAttribute && property.Type.IsNullable())
        {
            Raise(context, RedundantMaybeNullRule, location, property.Name);
        }
    }

    private static LocalizableResourceString GetResourceString(string name)
    {
        return new(name, ResourceManager, typeof(NullabilityAttributeAnalyzer_Resources));
    }

    private static IPropertySymbol? GetProperty(SyntaxNodeAnalysisContext context, AttributeSyntax syntax)
    {
        ISymbol? symbol = syntax.GetParent<PropertyDeclarationSyntax>(context)
            ?? syntax.GetParent<ParameterSyntax>(context);

        if (symbol is IPropertySymbol property)
        {
            return property;
        }

        if (symbol is IParameterSymbol parameter && parameter.ContainingType is INamedTypeSymbol type)
        {
            return type
                .GetMembers(parameter.Name)
                .OfType<IPropertySymbol>()
                .FirstOrDefault();
        }

        return default;
    }

    private static IMethodSymbol? GetSymbol(SyntaxNodeAnalysisContext context, AttributeSyntax syntax)
    {
        return context
            .SemanticModel
            .GetSymbolInfo(syntax, cancellationToken: context.CancellationToken)
            .Symbol as IMethodSymbol;
    }

    private static bool TryGetAttributeName(INamedTypeSymbol attribute, out string name)
    {
        name = attribute.Name switch
        {
            "AllowNullAttribute" => AllowNullAttribute,
            "DisallowNullAttribute" => DisallowNullAttribute,
            "MaybeNullAttribute" => MaybeNullAttribute,
            _ => string.Empty,
        };

        return !string.IsNullOrEmpty(name)
            && string.Equals(attribute.ContainingNamespace.ToDisplayString(), CodeAnalysisNamespace, StringComparison.Ordinal);
    }
}