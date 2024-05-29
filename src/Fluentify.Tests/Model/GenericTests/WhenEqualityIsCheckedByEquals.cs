namespace Fluentify.Model.GenericTests;

using Fluentify.Model;

public sealed class WhenEqualityIsCheckedByEquals
    : WhenEqualityIsChecked
{
    private protected override bool AreEqual(Generic? instance1, Generic? instance2)
    {
        if (instance1 is not null)
        {
            return instance1.Equals((object?)instance2);
        }

        return true;
    }
}