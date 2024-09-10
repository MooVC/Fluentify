namespace Fluentify.DescriptorAttributeAnalyzerTests;

using Fluentify.Snippets;
using Microsoft.CodeAnalysis.Text;

public partial class WhenExecuted
{
    public sealed class Record
        : WhenExecuted
    {
        public Record()
            : base(Records.ReferenceAssemblies, Records.LanguageVersion)
        {
        }

        [Theory]
        [InlineData("Valid")]
        [InlineData("inValid")]
        public async Task GivenADescriptorWhenFluentifyIsNotAppliedToTheRecordThenMissingFluentifyRuleIsRaised(string descriptor)
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedMissingFluentifyRule("TestRecord", new LinePosition(2, 26)));

            TestCode = $$"""
                using Fluentify;

                public record TestRecord([Descriptor("{{descriptor}}")] string Property);
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

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
        public async Task GivenAValidDescriptorWhenFluentifyIsAppliedToTheRecordThenNoDiagnosticIsRaised(string descriptor)
        {
            // Arrange
            TestCode = $$"""
                using Fluentify;

                [Fluentify]
                public record TestRecord([Descriptor("{{descriptor}}")] string Property);
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
        [InlineData("convertCurrency")]
        [InlineData("Fetch_Weather_Data")]
        [InlineData("Resize Image")]
        [InlineData("Process@Order")]
        public async Task GivenAnInvalidDescriptorWhenFluentifyIsAppliedToTheRecordThenTheValidNamingRuleDiagnosticIsRaised(string descriptor)
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedValidNamingRule(descriptor, new LinePosition(3, 37)));

            TestCode = $$"""
                using Fluentify;

                [Fluentify]
                public record TestRecord([Descriptor("{{descriptor}}")] string Property);
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

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
        public async Task GivenAValidDescriptorOnAnIgnoredPropertyWhenFluentifyIsAppliedToTheRecordThenDisregardedRuleIsRaised(string descriptor)
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedDisregardedRule("IgnoredPropertyWithValidDiscriptor", new LinePosition(3, 26)));

            TestCode = $$"""
                using Fluentify;

                [Fluentify]
                public record TestRecord([Descriptor("{{descriptor}}"), Ignore] string IgnoredPropertyWithValidDiscriptor);
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenASelfDescriptorOnANonBooleanWhenFluentifyIsAppliedToTheClassThenThenNoDiagnosticIsRaised()
        {
            // Arrange
            TestCode = $$"""
                using Fluentify;

                [Fluentify]
                public record TestRecord([Descriptor] string Name);
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenARedundantSelfDescriptorOnANonBooleanWhenFluentifyIsAppliedToTheClassThenDisregardedRuleIsRaised()
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedRedundantRule("WithName", "Name", new LinePosition(3, 26)));

            TestCode = $$"""
                using Fluentify;

                [Fluentify]
                public record TestRecord([Descriptor("WithName")] string Name);
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenASelfDescriptorOnABooleanWhenFluentifyIsAppliedToTheClassThenDisregardedRuleIsRaised()
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedRedundantRule("IsActive", "IsActive", new LinePosition(3, 26)));

            TestCode = $$"""
                using Fluentify;

                [Fluentify]
                public record TestRecord([Descriptor] bool IsActive);
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }
    }
}