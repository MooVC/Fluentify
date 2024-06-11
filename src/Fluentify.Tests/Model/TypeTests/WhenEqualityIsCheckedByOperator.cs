namespace Fluentify.Model.TypeTests;

using Fluentify.Model;

public sealed class WhenEqualityIsCheckedByOperator
    : WhenEqualityIsChecked
{
    private protected override bool AreEqual(Type? instance1, Type? instance2)
    {
        return instance1 == instance2;
    }
}