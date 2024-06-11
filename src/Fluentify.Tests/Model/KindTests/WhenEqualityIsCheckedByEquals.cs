namespace Fluentify.Model.KindTests;

using Fluentify.Model;

public sealed class WhenEqualityIsCheckedByEquals
    : WhenEqualityIsChecked
{
    private protected override bool AreEqual(Kind? instance1, Kind? instance2)
    {
        if (instance1 is not null)
        {
            return instance1.Equals((object?)instance2);
        }

        return true;
    }
}