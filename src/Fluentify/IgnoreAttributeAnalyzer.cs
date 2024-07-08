namespace Fluentify;

using Fluentify.Semantics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using static Fluentify.IgnoreAttributeAnalyzer_Resources;

/// <summary>
/// Analyzes usage of the IgnoreAttribute, ensuring the property is not already disregarded from consideration.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class IgnoreAttributeAnalyzer
    : AttributeAnalyzer<PropertyDeclarationSyntax>
{
    /// <summary>
    /// Facilitates construction of the analyzer.
    /// </summary>
    public IgnoreAttributeAnalyzer()
        : base(SyntaxKind.PropertyDeclaration, RedundantUsageRule)
    {
    }

    /// <summary>
    /// Gets the descriptor associated with the redundant usage rule (Fluentify03).
    /// </summary>
    internal static DiagnosticDescriptor RedundantUsageRule { get; } = new(
        "Fluentify03",
        GetResourceString(nameof(Title)),
        GetResourceString(nameof(MessageFormat)),
        "Design",
        DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: GetResourceString(nameof(Description)));

    /// <inheritdoc/>
    protected override void AnalyzeNode(SyntaxNodeAnalysisContext context, PropertyDeclarationSyntax syntax)
    {
        IPropertySymbol? property = context.SemanticModel.GetDeclaredSymbol(syntax);

        if (property is null || !property.HasIgnore() || IsValidUsageOfIgnoreAttribute(property))
        {
            return;
        }

        Raise(context, RedundantUsageRule, syntax.Identifier.GetLocation(), syntax.Identifier.Text);
    }

    private static LocalizableResourceString GetResourceString(string name)
    {
        return new(name, ResourceManager, typeof(IgnoreAttributeAnalyzer_Resources));
    }

    private static bool IsValidUsageOfIgnoreAttribute(IPropertySymbol property)
    {
        return property.IsMutable() && property.ContainingType.HasFluentify();
    }
}