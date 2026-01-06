namespace Fluentify;

using System.Collections.Immutable;
using Fluentify.Semantics;
using Fluentify.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using static Fluentify.AutoInitiateWithAttributeGenerator;
using static Fluentify.SkipAutoInitializationAttributeAnalyzer_Resources;

/// <summary>
/// Analyzes usage of AutoInitiateWith when SkipAutoInitialization is also applied to the same type.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class SkipAutoInitializationAttributeAnalyzer
    : AttributeAnalyzer
{
    /// <summary>
    /// Facilitates construction of the analyzer.
    /// </summary>
    public SkipAutoInitializationAttributeAnalyzer()
        : base(AutoInitiateWithAttributeGenerator.Name)
    {
    }

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(
        ConflictingAttributesRule);

    /// <summary>
    /// Gets the descriptor associated with the conflicting attributes rule (FLTFY10).
    /// </summary>
    /// <value>
    /// The descriptor associated with the conflicting attributes rule (FLTFY10).
    /// </value>
    internal static DiagnosticDescriptor ConflictingAttributesRule { get; } = new(
        "FLTFY10",
        GetResourceString(nameof(ConflictingAttributesRuleTitle)),
        GetResourceString(nameof(ConflictingAttributesRuleMessageFormat)),
        "Usage",
        DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: GetResourceString(nameof(ConflictingAttributesRuleDescription)),
        helpLinkUri: GetHelpLinkUri("FLTFY10"));

    /// <inheritdoc/>
    protected override void AnalyzeNode(SyntaxNodeAnalysisContext context, IMethodSymbol symbol, AttributeSyntax syntax)
    {
        ISymbol? parent = syntax.GetParent<TypeDeclarationSyntax>(context);

        if (parent is not INamedTypeSymbol type)
        {
            return;
        }

        if (!type.HasAttribute(SkipAutoInitializationAttributeGenerator.Name))
        {
            return;
        }

        Raise(context, ConflictingAttributesRule, syntax.GetLocation(), type.Name);
    }

    private static LocalizableResourceString GetResourceString(string name)
    {
        return new(name, ResourceManager, typeof(SkipAutoInitializationAttributeAnalyzer_Resources));
    }
}