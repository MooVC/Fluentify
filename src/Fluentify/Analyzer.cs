namespace Fluentify;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

/// <summary>
/// Supports the impelmentation of analyzers relating to the specified <typeparamref name="TSyntax"/>.
/// </summary>
/// <typeparam name="TSyntax">The type of syntax upon which the analyzer will be applied.</typeparam>
public abstract class Analyzer<TSyntax>
    : DiagnosticAnalyzer
    where TSyntax : CSharpSyntaxNode
{
    private const string Branch = "master";
    private readonly SyntaxKind _kind;

    /// <summary>
    /// Facilitates construction of an analyzer that matches for the specified <paramref name="kind"/>.
    /// </summary>
    /// <param name="kind">The type of syntax to match.</param>
    private protected Analyzer(SyntaxKind kind)
    {
        _kind = kind;
    }

    /// <inheritdoc/>
    public sealed override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeNode, _kind);
    }

    /// <summary>
    /// Generates a help link based on the known location within the GitHub Repository.
    /// </summary>
    /// <param name="ruleId">The Diagnostic Id for the Rule.</param>
    /// <returns>The link to the documentation on GitHub for the specified <paramref name="ruleId"/>.</returns>
    protected static string GetHelpLinkUri(string ruleId)
    {
        return $"https://github.com/MooVC/Fluentify/blob/{Branch}/docs/rules/{ruleId}.md";
    }

    /// <summary>
    /// Raises a <see cref="Diagnostic"/> instance within the specified <paramref name="context"/>.
    /// </summary>
    /// <param name="context">
    /// Context for a syntax node action. A syntax node action can use a Microsoft.CodeAnalysis.Diagnostics.SyntaxNodeAnalysisContext
    /// to report Microsoft.CodeAnalysis.Diagnostics for a Microsoft.CodeAnalysis.SyntaxNode.
    /// </param>
    /// <param name="descriptor">A <see cref="DiagnosticDescriptor"/> describing the diagnostic.</param>
    /// <param name="location">An location of the diagnostic.</param>
    /// <param name="arguments">Arguments to the message of the diagnostic.</param>
    protected static void Raise(SyntaxNodeAnalysisContext context, DiagnosticDescriptor descriptor, Location location, params object[] arguments)
    {
        var diagnostic = Diagnostic.Create(descriptor, location, arguments);

        context.ReportDiagnostic(diagnostic);
    }

    /// <summary>
    /// Performs analysis upon the <paramref name="syntax"/>, which is deemed to have matched the
    /// <typeparamref name="TSyntax"/> and <see cref="kind"/> values.
    /// </summary>
    /// <param name="context">
    /// Context for a syntax node action. A syntax node action can use a Microsoft.CodeAnalysis.Diagnostics.SyntaxNodeAnalysisContext
    /// to report Microsoft.CodeAnalysis.Diagnostics for a Microsoft.CodeAnalysis.SyntaxNode.
    /// </param>
    /// <param name="syntax">
    /// The <typeparamref name="TSyntax"/> matched within the <paramref name="context"/> to which the analysis is to be applied.
    /// </param>
    protected abstract void AnalyzeNode(SyntaxNodeAnalysisContext context, TSyntax syntax);

    private void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is TSyntax syntax)
        {
            AnalyzeNode(context, syntax);
        }
    }
}