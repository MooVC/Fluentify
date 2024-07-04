namespace Fluentify;

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
    : AttributeAnalyzer<ClassDeclarationSyntax>
{
    internal const string DiagnosticId = "FY0001";
    internal const string Category = "Design";

    private static readonly LocalizableString description = GetResourceString(nameof(Description));
    private static readonly LocalizableString messageFormat = GetResourceString(nameof(MessageFormat));
    private static readonly LocalizableString title = GetResourceString(nameof(Title));

    private static readonly DiagnosticDescriptor rule = new(
        DiagnosticId,
        title,
        messageFormat,
        Category,
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: description);

    /// <summary>
    /// Facilitates construction of the analyzer.
    /// </summary>
    public ClassAnalyzer()
        : base(SyntaxKind.ClassDeclaration, rule)
    {
    }

    /// <inheritdoc/>
    protected override void AnalyzeNode(SyntaxNodeAnalysisContext context, ClassDeclarationSyntax syntax)
    {
        INamedTypeSymbol? symbol = context.SemanticModel.GetDeclaredSymbol(syntax, context.CancellationToken);

        if (symbol is null || !symbol.HasFluentify() || symbol.HasAccessibleParameterlessConstructor(context.Compilation))
        {
            return;
        }

        Raise(context, rule, syntax.Identifier.GetLocation(), syntax.Identifier.Text);
    }

    private static LocalizableResourceString GetResourceString(string name)
    {
        return new(name, ResourceManager, typeof(ClassAnalyzer_Resources));
    }
}