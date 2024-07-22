namespace Fluentify.Model.SubjectTests;

using Fluentify.Model;

public sealed class WhenInequalityIsCheckedByOperator
    : WhenInequalityIsChecked
{
    private protected override bool AreNotEqual(Subject? instance1, Subject? instance2)
    {
        return instance1 != instance2;
    }
}