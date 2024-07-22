namespace Fluentify;

using Fluentify.Semantics;
using Fluentify.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using static Fluentify.DescriptorAttributeAnalyzer_Resources;
using static Fluentify.DescriptorAttributeGenerator;

/// <summary>
/// Analyzes usage of the DescriptorAttribute to ensure the value specified is suitable for use as a method name.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class DescriptorAttributeAnalyzer
    : AttributeAnalyzer
{
    /// <summary>
    /// Facilitates construction of the analyzer.
    /// </summary>
    public DescriptorAttributeAnalyzer()
        : base(Name, DisregardedRule, MissingFluentifyRule, ValidNamingRule)
    {
    }

    /// <summary>
    /// Gets the descriptor associated with the disregarded rule (FLTFY02).
    /// </summary>
    /// <value>
    /// The descriptor associated with the disregarded rule (FLTFY02).
    /// </value>
    internal static DiagnosticDescriptor DisregardedRule { get; } = new(
        "FLTFY02",
        GetResourceString(nameof(DisregardedRuleTitle)),
        GetResourceString(nameof(DisregardedRuleMessageFormat)),
        "Usage",
        DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: GetResourceString(nameof(DisregardedRuleDescription)),
        helpLinkUri: GetHelpLinkUri("FLTFY02"));

    /// <summary>
    /// Gets the descriptor associated with the missing fluentify rule (FLTFY03).
    /// </summary>
    /// <value>
    /// The descriptor associated with the missing fluentify rule (FLTFY03).
    /// </value>
    internal static DiagnosticDescriptor MissingFluentifyRule { get; } = new(
        "FLTFY03",
        GetResourceString(nameof(MissingFluentifyRuleTitle)),
        GetResourceString(nameof(MissingFluentifyRuleMessageFormat)),
        "Usage",
        DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: GetResourceString(nameof(MissingFluentifyRuleDescription)),
        helpLinkUri: GetHelpLinkUri("FLTFY03"));

    /// <summary>
    /// Gets the descriptor associated with the naming rule (FLTFY04).
    /// </summary>
    /// <value>
    /// The descriptor associated with the naming rule (FLTFY04).
    /// </value>
    internal static DiagnosticDescriptor ValidNamingRule { get; } = new(
        "FLTFY04",
        GetResourceString(nameof(ValidNamingRuleTitle)),
        GetResourceString(nameof(ValidNamingRuleMessageFormat)),
        "Naming",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: GetResourceString(nameof(ValidNamingRuleDescription)),
        helpLinkUri: GetHelpLinkUri("FLTFY04"));

    /// <inheritdoc/>
    protected override void AnalyzeNode(SyntaxNodeAnalysisContext context, IMethodSymbol symbol, AttributeSyntax syntax)
    {
        if (IsViolatingMissingFluentifyRule(context, syntax, out string @class, out Location location))
        {
            Raise(context, MissingFluentifyRule, location, @class);
        }
        else if (IsViolatingValidNamingRule(context, syntax, out string descriptor, out location))
        {
            Raise(context, ValidNamingRule, location, descriptor);
        }
        else if (IsViolatingDisregardedRule(context, syntax, out location, out string name))
        {
            Raise(context, DisregardedRule, location, name);
        }
    }

    private static LocalizableResourceString GetResourceString(string name)
    {
        return new(name, ResourceManager, typeof(DescriptorAttributeAnalyzer_Resources));
    }

    private static bool IsViolatingDisregardedRule(SyntaxNodeAnalysisContext context, AttributeSyntax syntax, out Location location, out string name)
    {
        ISymbol? symbol = syntax.GetParent<ParameterSyntax>(context)
            ?? syntax.GetParent<PropertyDeclarationSyntax>(context);

        if (symbol is null || !symbol.HasIgnore())
        {
            location = Location.None;
            name = string.Empty;

            return false;
        }

        location = syntax.GetLocation();
        name = symbol.Name;

        return true;
    }

    private static bool IsViolatingValidNamingRule(SyntaxNodeAnalysisContext context, AttributeSyntax syntax, out string descriptor, out Location location)
    {
        descriptor = string.Empty;
        location = Location.None;

        if (syntax.ArgumentList is null)
        {
            return false;
        }

        AttributeArgumentSyntax? argument = syntax.ArgumentList.Arguments.FirstOrDefault();

        if (argument is null)
        {
            return false;
        }

        Optional<object?> constant = context
            .SemanticModel
            .GetConstantValue(argument.Expression, cancellationToken: context.CancellationToken);

        if (!constant.HasValue || constant.Value is not string value || Pattern.IsMatch(value))
        {
            return false;
        }

        descriptor = value;
        location = argument.GetLocation();

        return true;
    }
}