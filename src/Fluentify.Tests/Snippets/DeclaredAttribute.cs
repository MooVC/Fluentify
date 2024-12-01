namespace Fluentify.Snippets;

using System.Reflection;
using Xunit.Sdk;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public sealed class DeclaredAttribute
    : DataAttribute
{
    public DeclaredAttribute(Type source)
    {
        Source = source;
    }

    public Type Source { get; }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        FieldInfo[] fields = Source.GetFields(BindingFlags.Public | BindingFlags.Static);

        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(Declared) && field.Name == "Nested")
            {
                object? value = field.GetValue(default);

                if (value is not null)
                {
                    yield return new object[] { value };
                }
            }
        }
    }
}