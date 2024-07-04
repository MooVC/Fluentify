namespace Fluentify;

using System.Collections.Immutable;
using Fluentify.Semantics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using static Fluentify.DescriptorAttributeAnalyzer_Resources;
using static Fluentify.DescriptorAttributeGenerator;

/// <summary>
/// Analyzes usage of the DescriptorAttribute to ensure the value specified is suitable for use as a method name.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class DescriptorAttributeAnalyzer
    : AttributeAnalyzer<AttributeSyntax>
{
    internal const string DiagnosticId = "FY0002";
    internal const string Category = "Naming";

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
    public DescriptorAttributeAnalyzer()
        : base(SyntaxKind.Attribute, rule)
    {
    }

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [rule];

    /// <inheritdoc/>
    protected override void AnalyzeNode(SyntaxNodeAnalysisContext context, AttributeSyntax syntax)
    {
        if (syntax.ArgumentList is null
         || context.SemanticModel.GetSymbolInfo(syntax, cancellationToken: context.CancellationToken).Symbol is not IMethodSymbol symbol
         || symbol.ContainingType is null
         || !symbol.ContainingType.IsAttribute(Name))
        {
            return;
        }

        AttributeArgumentSyntax? argument = syntax.ArgumentList.Arguments.FirstOrDefault();

        if (argument is null)
        {
            return;
        }

        Optional<object?> constant = context.SemanticModel.GetConstantValue(argument.Expression, cancellationToken: context.CancellationToken);

        if (!constant.HasValue || constant.Value is null)
        {
            return;
        }

        string value = constant.Value.ToString();

        if (!Pattern.IsMatch(value))
        {
            Raise(context, rule, argument.GetLocation(), value);
        }
    }

    private static LocalizableResourceString GetResourceString(string name)
    {
        return new(name, ResourceManager, typeof(DescriptorAttributeAnalyzer_Resources));
    }
}