namespace Fluentify;

using System.Collections.Immutable;
using Fluentify.Semantics;
using Fluentify.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using static Fluentify.IgnoreAttributeAnalyzer_Resources;
using static Fluentify.IgnoreAttributeGenerator;

/// <summary>
/// Analyzes usage of the IgnoreAttribute, ensuring the property is not already disregarded from consideration.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class IgnoreAttributeAnalyzer
    : AttributeAnalyzer
{
    /// <summary>
    /// Facilitates construction of the analyzer.
    /// </summary>
    public IgnoreAttributeAnalyzer()
        : base(Name)
    {
    }

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(
        MissingFluentifyRule,
        RedundantUsageRule);

    /// <summary>
    /// Gets the descriptor associated with the missing fluentify rule (FLTFY05).
    /// </summary>
    /// <value>
    /// The descriptor associated with the missing fluentify rule (FLTFY05).
    /// </value>
    internal static DiagnosticDescriptor MissingFluentifyRule { get; } = new(
        "FLTFY05",
        GetResourceString(nameof(MissingFluentifyRuleTitle)),
        GetResourceString(nameof(MissingFluentifyRuleMessageFormat)),
        "Usage",
        DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: GetResourceString(nameof(MissingFluentifyRuleDescription)),
        helpLinkUri: GetHelpLinkUri("FLTFY05"));

    /// <summary>
    /// Gets the descriptor associated with the redundant usage rule (FLTFY06).
    /// </summary>
    /// <value>
    /// The descriptor associated with the redundant usage rule (FLTFY06).
    /// </value>
    internal static DiagnosticDescriptor RedundantUsageRule { get; } = new(
        "FLTFY06",
        GetResourceString(nameof(RedundantUsageRuleTitle)),
        GetResourceString(nameof(RedundantUsageRuleMessageFormat)),
        "Usage",
        DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: GetResourceString(nameof(RedundantUsageRuleDescription)),
        helpLinkUri: GetHelpLinkUri("FLTFY06"));

    /// <inheritdoc/>
    protected override void AnalyzeNode(SyntaxNodeAnalysisContext context, IMethodSymbol symbol, AttributeSyntax syntax)
    {
        if (IsViolatingMissingFluentifyRule(context, syntax, out string @class, out Location location))
        {
            Raise(context, MissingFluentifyRule, location, @class);
        }
        else if (IsViolatingRedundantUsageRule(context, syntax, out location, out string property))
        {
            Raise(context, RedundantUsageRule, location, property);
        }
    }

    private static LocalizableResourceString GetResourceString(string name)
    {
        return new(name, ResourceManager, typeof(IgnoreAttributeAnalyzer_Resources));
    }

    private static bool IsViolatingRedundantUsageRule(SyntaxNodeAnalysisContext context, AttributeSyntax syntax, out Location location, out string property)
    {
        ISymbol? symbol = syntax.GetParent(context, out PropertyDeclarationSyntax? node);

        if (node is null || symbol is not IPropertySymbol declaration || declaration.IsMutable())
        {
            location = Location.None;
            property = string.Empty;

            return false;
        }

        location = node.GetLocation();
        property = declaration.Name;

        return true;
    }
}