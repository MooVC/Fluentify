namespace Fluentify;

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using static Fluentify.DescriptorAttributeAnalyzer_Resources;
using static Fluentify.DescriptorAttributeGenerator;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class DescriptorAttributeAnalyzer
    : AttributeAnalyzer
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

    public DescriptorAttributeAnalyzer()
        : base(rule)
    {
    }

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [rule];

    protected override void AnalyzeNode(SyntaxNodeAnalysisContext context, IMethodSymbol symbol, AttributeSyntax syntax)
    {
        if (syntax.ArgumentList is null)
        {
            return;
        }

        AttributeArgumentSyntax? argument = syntax.ArgumentList.Arguments.FirstOrDefault();

        if (argument is null)
        {
            return;
        }

        Optional<object?> constant = context.SemanticModel.GetConstantValue(argument.Expression);

        if (!constant.HasValue || constant.Value is null)
        {
            return;
        }

        string value = constant.Value.ToString();

        if (!Pattern.IsMatch(value))
        {
            var diagnostic = Diagnostic.Create(rule, argument.GetLocation(), value);
            context.ReportDiagnostic(diagnostic);
        }
    }

    private static LocalizableResourceString GetResourceString(string name)
    {
        return new(name, ResourceManager, typeof(DescriptorAttributeAnalyzer_Resources));
    }
}