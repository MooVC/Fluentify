namespace Fluentify.Syntax.SubjectExtensionsTests;

using Fluentify.Model;
using MooVC.Syntax.CSharp;
using Generic = Fluentify.Model.Generic;

public sealed class WhenGetExtensionDeclarationIsCalled
{
    [Test]
    public void GivenContainingAndSubjectTypeParametersThenPreservesConstraintOrder()
    {
        var subject = new Subject
        {
            Nesting =
            [
                new Nesting
                {
                    Generics = [new Generic { Name = "T", Constraints = ["class", "global::System.IComparable<T>", "new()"] }],
                },
            ],
            Generics =
            [
                new Generic { Name = "U", Constraints = ["struct"] },
                new Generic { Name = "V" },
            ],
        };

        Declaration declaration = subject.GetExtensionDeclaration("With");

        declaration.ToSnippet(Configuration.Options).ToString().ShouldBe("With<T, U, V>");
        declaration.Arguments[0].ToSnippet(Configuration.Options.Types.Qualifications).ToString()
            .ShouldBe("where T : class, global::System.IComparable<T>, new()");
        declaration.Arguments[1].ToSnippet(Configuration.Options.Types.Qualifications).ToString().ShouldBe("where U : struct");
        declaration.Arguments[2].Constraints.ShouldBeEmpty();
    }
}