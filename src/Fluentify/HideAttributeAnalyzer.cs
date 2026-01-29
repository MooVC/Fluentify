namespace Fluentify;

using System.Collections.Immutable;
using Fluentify.Semantics;
using Fluentify.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using static Fluentify.HideAttributeAnalyzer_Resources;
using static Fluentify.HideAttributeGenerator;

/// <summary>
/// Analyzes usage of the HideAttribute, ensuring the property is applicable to Fluentify and does not conflict with Ignore.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class HideAttributeAnalyzer
    : AttributeAnalyzer
{
    /// <summary>
    /// Facilitates construction of the analyzer.
    /// </summary>
    public HideAttributeAnalyzer()
        : base(Name)
    {
    }

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(
        MissingFluentifyRule,
        ConflictingAttributesRule);

    /// <summary>
    /// Gets the descriptor associated with the missing fluentify rule (FLTFY11).
    /// </summary>
    /// <value>
    /// The descriptor associated with the missing fluentify rule (FLTFY11).
    /// </value>
    internal static DiagnosticDescriptor MissingFluentifyRule { get; } = new(
        "FLTFY11",
        GetResourceString(nameof(MissingFluentifyRuleTitle)),
        GetResourceString(nameof(MissingFluentifyRuleMessageFormat)),
        "Usage",
        DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: GetResourceString(nameof(MissingFluentifyRuleDescription)),
        helpLinkUri: GetHelpLinkUri("FLTFY11"));

    /// <summary>
    /// Gets the descriptor associated with the conflicting attributes rule (FLTFY12).
    /// </summary>
    /// <value>
    /// The descriptor associated with the conflicting attributes rule (FLTFY12).
    /// </value>
    internal static DiagnosticDescriptor ConflictingAttributesRule { get; } = new(
        "FLTFY12",
        GetResourceString(nameof(ConflictingAttributesRuleTitle)),
        GetResourceString(nameof(ConflictingAttributesRuleMessageFormat)),
        "Usage",
        DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: GetResourceString(nameof(ConflictingAttributesRuleDescription)),
        helpLinkUri: GetHelpLinkUri("FLTFY12"));

    /// <inheritdoc/>
    protected override void AnalyzeNode(SyntaxNodeAnalysisContext context, IMethodSymbol symbol, AttributeSyntax syntax)
    {
        if (IsViolatingMissingFluentifyRule(context, syntax, out string property, out string _, out Location location))
        {
            Raise(context, MissingFluentifyRule, location, property);
        }
        else if (IsViolatingConflictingAttributesRule(context, syntax, out location, out property))
        {
            Raise(context, ConflictingAttributesRule, location, property);
        }
    }

    private static LocalizableResourceString GetResourceString(string name)
    {
        return new(name, ResourceManager, typeof(HideAttributeAnalyzer_Resources));
    }

    private static bool IsViolatingConflictingAttributesRule(
        SyntaxNodeAnalysisContext context,
        AttributeSyntax syntax,
        out Location location,
        out string property)
    {
        ISymbol? symbol = syntax.GetParent<PropertyDeclarationSyntax>(context)
            ?? syntax.GetParent<ParameterSyntax>(context);

        if (symbol is null || !symbol.HasIgnore())
        {
            location = Location.None;
            property = string.Empty;

            return false;
        }

        location = syntax.GetLocation();
        property = symbol.Name;

        return true;
    }
}