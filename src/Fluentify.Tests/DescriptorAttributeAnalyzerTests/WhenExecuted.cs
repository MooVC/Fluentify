namespace Fluentify.DescriptorAttributeAnalyzerTests;

using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;

public sealed class WhenExecuted
    : AnalyzerTests<DescriptorAttributeAnalyzer, DescriptorAttributeGenerator>
{
    [Theory]
    [InlineData("CalculateTotalPrice")]
    [InlineData("GenerateReport")]
    [InlineData("SendEmailNotification")]
    [InlineData("ValidateUserCredentials")]
    [InlineData("ParseJsonResponse")]
    [InlineData("UpdateDatabaseRecord")]
    [InlineData("ConvertCurrency")]
    [InlineData("FetchWeatherData")]
    [InlineData("ResizeImage")]
    [InlineData("ProcessOrder")]
    public async Task GivenAValidDescriptorWhenFluentifyIsAppliedThenNoDiagnosticIsRaised(string descriptor)
    {
        // Arrange
        TestCode = $$"""
            using Fluentify;

            [Fluentify]
            public class TestClass
            {
                [Descriptor("{{descriptor}}")]
                public string Property { get; set; }
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Theory]
    [InlineData("Calculate-Total-Price")]
    [InlineData("123GenerateReport")]
    [InlineData("Send Email Notification")]
    [InlineData("ValidateUser$Credentials")]
    [InlineData("ParseJson.Response")]
    [InlineData("Update Database Record")]
    [InlineData("ConvertCurrency!")]
    [InlineData("Fetch_Weather_Data")]
    [InlineData("Resize Image")]
    [InlineData("Process@Order")]
    public async Task GivenAnInvalidDescriptorWhenFluentifyIsAppliedThenTheValidNamingRuleDiagnosticIsRaised(string descriptor)
    {
        // Arrange
        ExpectedDiagnostics.Add(GetExpectedValidNamingRule(descriptor, new LinePosition(5, 16)));

        TestCode = $$"""
            using Fluentify;

            [Fluentify]
            public class TestClass
            {
                [Descriptor("{{descriptor}}")]
                public string Property { get; set; }
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    private static DiagnosticResult GetExpectedValidNamingRule(string descriptor, LinePosition position)
    {
        return new DiagnosticResult(DescriptorAttributeAnalyzer.ValidNamingRule)
            .WithArguments(descriptor)
            .WithLocation(position);
    }
}