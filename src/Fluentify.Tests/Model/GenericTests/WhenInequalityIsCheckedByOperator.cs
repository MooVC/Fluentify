namespace Fluentify.Model.GenericTests;

using Fluentify.Model;

public sealed class WhenInequalityIsCheckedByOperator
    : WhenInequalityIsChecked
{
    private protected override bool AreNotEqual(Generic? instance1, Generic? instance2)
    {
        return instance1 != instance2;
    }
}