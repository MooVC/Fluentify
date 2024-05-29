namespace Fluentify.Model.PropertyTests;

using Fluentify.Model;

public sealed class WhenInequalityIsCheckedByOperator
    : WhenInequalityIsChecked
{
    private protected override bool AreNotEqual(Property? instance1, Property? instance2)
    {
        return instance1 != instance2;
    }
}