namespace Fluentify;

using System.Collections.Immutable;
using Fluentify.Semantics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using static Fluentify.RecordAnalyzer_Resources;

/// <summary>
/// Analyzes usage of the FluentifyAttribute when applied to a record, ensuring the record can support a generated default constructor.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class RecordAnalyzer
    : Analyzer<RecordDeclarationSyntax>
{
    /// <summary>
    /// Facilitates construction of the analyzer.
    /// </summary>
    public RecordAnalyzer()
        : base(SyntaxKind.RecordDeclaration)
    {
    }

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(PartialRecordRequiredRule);

    /// <summary>
    /// Gets the descriptor associated with the partial record rule (FLTFY08).
    /// </summary>
    /// <value>
    /// The descriptor associated with the partial record rule (FLTFY08).
    /// </value>
    internal static DiagnosticDescriptor PartialRecordRequiredRule { get; } = new(
        "FLTFY08",
        GetResourceString(nameof(PartialRecordRequiredRuleTitle)),
        GetResourceString(nameof(PartialRecordRequiredRuleMessageFormat)),
        "Design",
        DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: GetResourceString(nameof(PartialRecordRequiredRuleDescription)),
        helpLinkUri: GetHelpLinkUri("FLTFY08"));

    /// <inheritdoc/>
    protected override void AnalyzeNode(SyntaxNodeAnalysisContext context, RecordDeclarationSyntax syntax)
    {
        INamedTypeSymbol? symbol = context.SemanticModel.GetDeclaredSymbol(syntax, context.CancellationToken);

        if (symbol is not null
            && symbol.HasFluentify()
            && !symbol.HasAccessibleParameterlessConstructor(context.Compilation)
            && !syntax.Modifiers.Any(SyntaxKind.PartialKeyword))
        {
            Raise(context, PartialRecordRequiredRule, syntax.Identifier.GetLocation(), syntax.Identifier.Text);
        }
    }

    private static LocalizableResourceString GetResourceString(string name)
    {
        return new(name, ResourceManager, typeof(RecordAnalyzer_Resources));
    }
}