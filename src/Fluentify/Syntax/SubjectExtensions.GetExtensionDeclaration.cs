namespace Fluentify.Syntax;

using Fluentify.Model;
using MooVC.Syntax.CSharp;
using Generic = MooVC.Syntax.CSharp.Generic;
using TypeParameter = Fluentify.Model.Generic;

internal static partial class SubjectExtensions
{
    public static Declaration GetExtensionDeclaration(this Subject subject, string name)
    {
        Generic[] arguments = subject.Nesting
            .SelectMany(parent => parent.Generics)
            .Concat(subject.Generics)
            .Select(ToGeneric)
            .ToArray();

        return Declaration.Unspecified.Named(name).WithArguments(arguments);
    }

    private static Generic ToGeneric(TypeParameter parameter)
    {
        var interfaces = new List<Implementation>();
        Constraint constraint = Constraint.Unspecified;

        foreach (string restriction in parameter.Constraints)
        {
            switch (restriction)
            {
                case "class":
                    constraint = constraint.WithNature(Natures.Class);
                    break;
                case "struct":
                    constraint = constraint.WithNature(Natures.Struct);
                    break;
                case "notnull":
                    constraint = constraint.WithNature(Natures.NotNull);
                    break;
                case "new()":
                    constraint = constraint.WithNew(New.Required);
                    break;
                default:
                    interfaces.Add(Implementation.Unspecified.Named(restriction.ToTypeSymbol().Name));
                    break;
            }
        }

        Generic argument = Generic.Undefined.Named(parameter.Name);

        return parameter.Constraints.Count == 0
            ? argument
            : argument.WithConstraints(constraint.WithInterfaces(interfaces.ToArray()));
    }
}