namespace Fluentify;

using System.Collections.Immutable;
using Fluentify.Semantics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using static Fluentify.DescriptorAttributeGenerator;

public abstract class AttributeAnalyzer
    : DiagnosticAnalyzer
{
    private protected AttributeAnalyzer(params DiagnosticDescriptor[] diagnostics)
    {
        SupportedDiagnostics = ImmutableArray.Create(diagnostics);
    }

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }

    public sealed override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.Attribute);
    }

    protected abstract void AnalyzeNode(SyntaxNodeAnalysisContext context, IMethodSymbol symbol, AttributeSyntax syntax);

    private void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        var syntax = (AttributeSyntax)context.Node;

        if (context.SemanticModel.GetSymbolInfo(syntax).Symbol is not IMethodSymbol symbol
         || symbol.ContainingType is null)
        {
            return;
        }

        if (symbol.ContainingType.IsAttribute(Name))
        {
            AnalyzeNode(context, symbol, syntax);
        }
    }
}