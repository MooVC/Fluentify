﻿namespace Fluentify;

using System.Collections.Immutable;
using Fluentify.Semantics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using static Fluentify.ClassAnalyzer_Resources;

/// <summary>
/// Analyzes usage of the FluentifyAttribute when applied to a class, ensuring the class adheres to the known constraints.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class ClassAnalyzer
    : Analyzer<ClassDeclarationSyntax>
{
    /// <summary>
    /// Facilitates construction of the analyzer.
    /// </summary>
    public ClassAnalyzer()
        : base(SyntaxKind.ClassDeclaration)
    {
    }

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [AccessibleDefaultConstructorRule];

    /// <summary>
    /// Gets the descriptor associated with accessible default constructor rule (FLTFY01).
    /// </summary>
    /// <value>
    /// The descriptor associated with accessible default constructor rule (FLTFY01).
    /// </value>
    internal static DiagnosticDescriptor AccessibleDefaultConstructorRule { get; } = new(
        "FLTFY01",
        GetResourceString(nameof(AccessibleDefaultConstructorRuleTitle)),
        GetResourceString(nameof(AccessibleDefaultConstructorRuleMessageFormat)),
        "Design",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: GetResourceString(nameof(AccessibleDefaultConstructorRuleDescription)),
        helpLinkUri: GetHelpLinkUri("FLTFY01"));

    /// <inheritdoc/>
    protected override void AnalyzeNode(SyntaxNodeAnalysisContext context, ClassDeclarationSyntax syntax)
    {
        INamedTypeSymbol? symbol = context.SemanticModel.GetDeclaredSymbol(syntax, context.CancellationToken);

        if (symbol is null || !symbol.HasFluentify() || symbol.HasAccessibleParameterlessConstructor(context.Compilation))
        {
            return;
        }

        Raise(context, AccessibleDefaultConstructorRule, syntax.Identifier.GetLocation(), syntax.Identifier.Text);
    }

    private static LocalizableResourceString GetResourceString(string name)
    {
        return new(name, ResourceManager, typeof(ClassAnalyzer_Resources));
    }
}