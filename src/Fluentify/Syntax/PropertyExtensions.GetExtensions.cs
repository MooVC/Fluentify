namespace Fluentify.Syntax;

using Fluentify.Model;
using Microsoft.CodeAnalysis;
using MooVC.Syntax;
using MooVC.Syntax.CSharp;
using Property = Fluentify.Model.Property;
using Type = Fluentify.Model.Type;

/// <summary>
/// Provides extensions relating to <see cref="Property"/>.
/// </summary>
internal static partial class PropertyExtensions
{
    /// <summary>
    /// Gets the source code for the fluent overloads associated with a property.
    /// </summary>
    /// <param name="property">The property to extend.</param>
    /// <param name="subject">The subject to which the property belongs.</param>
    /// <param name="scalar">The function that renders the property assignment.</param>
    /// <returns>The extension source, or an empty snippet when no overloads were generated.</returns>
    public static Snippet GetExtensions(this Property property, Subject subject, Func<Property, string?> scalar)
    {
        bool supportsNullableReferenceTypes = subject.SupportsNullableReferenceTypes;
        Kind kind = property.Kind;
        Type type = kind.Pattern == Pattern.Scalar ? kind.Type : kind.Member;
        Symbol value = kind.Type.ToSymbol(supportsNullableReferenceTypes);
        Symbol member = type.ToSymbol(supportsNullableReferenceTypes);
        Symbol function = (Name: "Func", Qualifier: "System");
        function = function.WithArguments(member, member);

        Parameter builder = Parameter.Undefined.Named("Builder").OfType(function);
        Symbol array = kind.Pattern == Pattern.Scalar ? Symbol.Undefined : $"{type.ToString(supportsNullableReferenceTypes)}[]".ToTypeSymbol();
        Parameter values = Parameter.Undefined.Named("Values").OfType(array).WithModifier(Parameter.Modes.Params);
        Parameter instance = Parameter.Undefined.Named("Instance").OfType(member);
        Parameter subjectParameter = Parameter.Undefined.Named("Subject")
            .OfType(subject.Type.ToSymbol(supportsNullableReferenceTypes))
            .WithModifier(Parameter.Modes.This);

        Scopes scope = subject.Accessibility < Accessibility.Public || property.Accessibility < Accessibility.Public || property.IsHidden
            ? Scopes.Internal
            : Scopes.Public;

        Method method = Method.Undefined
            .Named(subject.GetExtensionDeclaration(property.Descriptor))
            .WithExtensibility(Modifiers.Static)
            .WithScope(scope)
            .Accepts(subjectParameter)
            .Returns(Result.Void.OfType(subjectParameter.Type));

        var methods = new List<Method>();

        AddMethod(property.GetArrayExtensionMethodBody(scalar), values);
        AddMethod(property.GetCollectionExtensionMethodBody(scalar), values);
        AddMethod(property.GetDelegateAndValuesExtensionMethodBody(type), builder, values);
        AddMethod(property.GetDelegateAndInstanceExtensionMethodBody(type), instance, builder);
        AddMethod(property.GetDelegateExtensionMethodBody(type), builder);
        AddMethod(property.GetEnumerableExtensionMethodBody(scalar), values);
        AddMethod(property.GetScalarExtensionMethodBody(scalar), Parameter.Undefined.Named("Value").OfType(value));

        return methods.Count == 0 ? Snippet.Empty : subject.ToExtensionSource(methods.ToArray());

        void AddMethod(string? body, params Parameter[] parameters)
        {
            if (body is null)
            {
                return;
            }

            string separator = StartsWithParameterNullGuard(body) ? "\n" : "\n\n";
            string content = string.Concat(GetParameterNullCheck("subject", "subject"), separator, body.TrimEnd());
            methods.Add(method.Accepts(parameters).WithBody(Snippet.From(Configuration.Options, content)));
        }
    }

    private static bool StartsWithParameterNullGuard(string body)
    {
        return body.StartsWith(GetParameterNullCheck("builder", "builder"), StringComparison.Ordinal)
            || body.StartsWith(GetParameterNullCheck("values", "values"), StringComparison.Ordinal);
    }
}