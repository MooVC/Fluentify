namespace Fluentify.Model.PropertyTests;

using Fluentify.Model;

public sealed class WhenEqualityIsCheckedByEquals
    : WhenEqualityIsChecked
{
    private protected override bool AreEqual(Property? instance1, Property? instance2)
    {
        if (instance1 is not null)
        {
            return instance1.Equals((object?)instance2);
        }

        return true;
    }
}