namespace Fluentify;

using System.Collections.Immutable;
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
        : base(Name)
    {
    }

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(
        DisregardedRule,
        MissingFluentifyRule,
        RedundantRule,
        ValidNamingRule);

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

    /// <summary>
    /// Gets the descriptor associated with the redundant rule (FLTFY07).
    /// </summary>
    /// <value>
    /// The descriptor associated with the redundant rule (FLTFY07).
    /// </value>
    internal static DiagnosticDescriptor RedundantRule { get; } = new(
        "FLTFY07",
        GetResourceString(nameof(RedundantRuleTitle)),
        GetResourceString(nameof(RedundantRuleMessageFormat)),
        "Usage",
        DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: GetResourceString(nameof(RedundantRuleDescription)),
        helpLinkUri: GetHelpLinkUri("FLTFY07"));

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
        else if (IsViolatingDisregardedRule(context, syntax, out location, out string name, out ISymbol? member))
        {
            Raise(context, DisregardedRule, location, name);
        }
        else if (IsViolatingRedundantRule(member!, syntax, ref descriptor, out location, out name))
        {
            Raise(context, RedundantRule, location, descriptor, name);
        }
    }

    private static bool IsViolatingRedundantRule(ISymbol symbol, AttributeSyntax syntax, ref string descriptor, out Location location, out string name)
    {
        if (IsSelfDescribing(symbol, ref descriptor))
        {
            location = syntax.GetLocation();
            name = symbol.Name;

            return true;
        }

        location = Location.None;
        name = string.Empty;

        return false;
    }

    private static LocalizableResourceString GetResourceString(string name)
    {
        return new(name, ResourceManager, typeof(DescriptorAttributeAnalyzer_Resources));
    }

    private static bool HasDescriptor(AttributeArgumentSyntax argument, SyntaxNodeAnalysisContext context, out string descriptor)
    {
        Optional<object?> constant = context
            .SemanticModel
            .GetConstantValue(argument.Expression, cancellationToken: context.CancellationToken);

        if (!constant.HasValue || constant.Value is not string value)
        {
            descriptor = string.Empty;

            return false;
        }

        descriptor = value;

        return true;
    }

    private static bool IsSelfDescribing(ISymbol symbol, ref string descriptor)
    {
        string @default = symbol.GetDefaultDescriptor();

        if (string.IsNullOrEmpty(descriptor))
        {
            descriptor = symbol.Name;
        }

        return string.Equals(@default, descriptor, StringComparison.Ordinal);
    }

    private static bool IsViolatingDisregardedRule(
        SyntaxNodeAnalysisContext context,
        AttributeSyntax syntax,
        out Location location,
        out string name,
        out ISymbol? symbol)
    {
        symbol = syntax.GetParent<ParameterSyntax>(context)
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

        if (HasDescriptor(argument, context, out descriptor) && Pattern.IsMatch(descriptor))
        {
            return false;
        }

        location = argument.GetLocation();

        return true;
    }
}