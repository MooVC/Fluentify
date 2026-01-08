namespace Fluentify;

using System.Collections.Immutable;
using System.Linq;
using Fluentify.Semantics;
using Fluentify.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using static Fluentify.AutoInitiateWithAttributeAnalyzer_Resources;
using static Fluentify.AutoInitiateWithAttributeGenerator;

/// <summary>
/// Analyzes usage of the AutoInitiateWith attribute to ensure the referenced member exists and is valid.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class AutoInitiateWithAttributeAnalyzer
    : AttributeAnalyzer
{
    /// <summary>
    /// Facilitates construction of the analyzer.
    /// </summary>
    public AutoInitiateWithAttributeAnalyzer()
        : base(Name)
    {
    }

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(
        InvalidTargetRule);

    /// <summary>
    /// Gets the descriptor associated with the invalid target rule (FLTFY09).
    /// </summary>
    /// <value>
    /// The descriptor associated with the invalid target rule (FLTFY09).
    /// </value>
    internal static DiagnosticDescriptor InvalidTargetRule { get; } = new(
        "FLTFY09",
        GetResourceString(nameof(InvalidTargetRuleTitle)),
        GetResourceString(nameof(InvalidTargetRuleMessageFormat)),
        "Usage",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: GetResourceString(nameof(InvalidTargetRuleDescription)),
        helpLinkUri: GetHelpLinkUri("FLTFY09"));

    /// <inheritdoc/>
    protected override void AnalyzeNode(SyntaxNodeAnalysisContext context, IMethodSymbol symbol, AttributeSyntax syntax)
    {
        if (!TryGetTarget(context, syntax, out AttributeData? attribute, out ITypeSymbol? type))
        {
            return;
        }

        string member = string.Empty;

        if (attribute is not null && !attribute.TryGetMember(out member))
        {
            return;
        }

        if (type.TryResolve(type, ref member, out _))
        {
            return;
        }

        string name = type!.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);

        Raise(context, InvalidTargetRule, syntax.GetLocation(), name, member);
    }

    private static AttributeData? GetAttribute(SyntaxNodeAnalysisContext context, AttributeSyntax syntax, ISymbol target)
    {
        return target
            .GetAttributes()
            .FirstOrDefault(attribute => attribute.ApplicationSyntaxReference?.GetSyntax(context.CancellationToken) == syntax);
    }

    private static LocalizableResourceString GetResourceString(string name)
    {
        return new(name, ResourceManager, typeof(AutoInitiateWithAttributeAnalyzer_Resources));
    }

    private static bool TryGetTarget(SyntaxNodeAnalysisContext context, AttributeSyntax syntax, out AttributeData? attribute, out ITypeSymbol? type)
    {
        attribute = default;
        type = default;

        ISymbol? target = syntax.GetParent<PropertyDeclarationSyntax>(context)
            ?? syntax.GetParent<ParameterSyntax>(context)
            ?? syntax.GetParent<TypeDeclarationSyntax>(context);

        if (target is null)
        {
            return false;
        }

        attribute = GetAttribute(context, syntax, target);

        if (attribute is null)
        {
            return false;
        }

        type = target switch
        {
            IPropertySymbol property => property.Type,
            IParameterSymbol parameter => parameter.Type,
            INamedTypeSymbol named => named,
            _ => default,
        };

        return type is not null;
    }
}