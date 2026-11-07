namespace Fluentify.Model.NestingTests;

[InheritsTests]
public sealed class WhenInequalityIsCheckedByOperator
    : WhenInequalityIsChecked
{
    private protected override bool AreNotEqual(Nesting? instance1, Nesting? instance2)
    {
        return instance1 != instance2;
    }
}