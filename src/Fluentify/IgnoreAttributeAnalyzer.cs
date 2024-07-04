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
    internal const string DiagnosticId = "FY0003";
    internal const string Category = "Design";

    private static readonly LocalizableString description = GetResourceString(nameof(Description));
    private static readonly LocalizableString messageFormat = GetResourceString(nameof(MessageFormat));
    private static readonly LocalizableString title = GetResourceString(nameof(Title));

    private static readonly DiagnosticDescriptor rule = new(
        DiagnosticId,
        title,
        messageFormat,
        Category,
        DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: description);

    /// <summary>
    /// Facilitates construction of the analyzer.
    /// </summary>
    public IgnoreAttributeAnalyzer()
        : base(SyntaxKind.PropertyDeclaration, rule)
    {
    }

    /// <inheritdoc/>
    protected override void AnalyzeNode(SyntaxNodeAnalysisContext context, PropertyDeclarationSyntax syntax)
    {
        IPropertySymbol? property = context.SemanticModel.GetDeclaredSymbol(syntax);

        if (property is null || property.IsMutable())
        {
            return;
        }

        Raise(context, rule, syntax.Identifier.GetLocation(), syntax.Identifier.Text);
    }

    private static LocalizableResourceString GetResourceString(string name)
    {
        return new(name, ResourceManager, typeof(IgnoreAttributeAnalyzer_Resources));
    }
}