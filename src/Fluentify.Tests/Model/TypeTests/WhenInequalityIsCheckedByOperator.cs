namespace Fluentify.Model.TypeTests;

using Fluentify.Model;

public sealed class WhenInequalityIsCheckedByOperator
    : WhenInequalityIsChecked
{
    private protected override bool AreNotEqual(Type? instance1, Type? instance2)
    {
        return instance1 != instance2;
    }
}