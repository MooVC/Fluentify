namespace Fluentify.Model.KindTests;

using Fluentify.Model;

public sealed class WhenEqualityIsCheckedByOperator
    : WhenEqualityIsChecked
{
    private protected override bool AreEqual(Kind? instance1, Kind? instance2)
    {
        return instance1 == instance2;
    }
}