namespace Fluentify.Model.GenericTests;

using Fluentify.Model;

[InheritsTests]
public sealed class WhenEqualityIsCheckedByEquatable
    : WhenEqualityIsChecked
{
    private protected override bool AreEqual(Generic? instance1, Generic? instance2)
    {
        if (instance1 is not null)
        {
            return instance1.Equals(instance2);
        }

        return true;
    }
}