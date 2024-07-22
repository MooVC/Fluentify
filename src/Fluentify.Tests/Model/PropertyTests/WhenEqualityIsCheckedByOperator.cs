namespace Fluentify.Model.PropertyTests;

using Fluentify.Model;

public sealed class WhenEqualityIsCheckedByOperator
    : WhenEqualityIsChecked
{
    private protected override bool AreEqual(Property? instance1, Property? instance2)
    {
        return instance1 == instance2;
    }
}