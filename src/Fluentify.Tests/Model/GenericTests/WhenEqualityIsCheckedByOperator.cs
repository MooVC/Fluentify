namespace Fluentify.Model.GenericTests;

using Fluentify.Model;

public sealed class WhenEqualityIsCheckedByOperator
    : WhenEqualityIsChecked
{
    private protected override bool AreEqual(Generic? instance1, Generic? instance2)
    {
        return instance1 == instance2;
    }
}