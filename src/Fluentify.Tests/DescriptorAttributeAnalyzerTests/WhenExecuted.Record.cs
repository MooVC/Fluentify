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

        [Test]
        [Arguments("Valid")]
        [Arguments("inValid")]
        public async Task GivenADescriptorOnASealedRecordWhenFluentifyIsNotAppliedThenMissingFluentifyRuleIsRaised(string descriptor)
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedMissingFluentifyRule("Property", "TestRecord", new LinePosition(2, 33)));

            TestCode = $$"""
                using Fluentify;

                public sealed record TestRecord([Descriptor("{{descriptor}}")] string Property);
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Test]
        [Arguments("Valid")]
        public async Task GivenADescriptorOnANonSealedRecordWhenFluentifyIsNotAppliedThenNoDiagnosticIsRaised(string descriptor)
        {
            // Arrange
            TestCode = $$"""
                using Fluentify;

                public record TestRecord([Descriptor("{{descriptor}}")] string Property);
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Test]
        public async Task GivenADuplicateDescriptorWhenFluentifyIsAppliedToTheRecordThenDuplicateDescriptorRuleIsRaised()
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedDuplicateDescriptorRule("Update", "SecondProperty", "FirstProperty", "TestRecord", new LinePosition(3, 71)));

            TestCode = $$"""
                using Fluentify;

                [Fluentify]
                public record TestRecord([Descriptor("Update")] string FirstProperty, [Descriptor("Update")] string SecondProperty);
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Test]
        public async Task GivenAPropertySharingADescriptorWithAnIgnoredPropertyWhenFluentifyIsAppliedToTheRecordThenNoDuplicateDescriptorRuleIsRaised()
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedDisregardedRule("FirstProperty", new LinePosition(3, 26)));

            TestCode = $$"""
                using Fluentify;

                [Fluentify]
                public record TestRecord([Descriptor("Update"), Ignore] string FirstProperty, [Descriptor("Update")] string SecondProperty);
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
        [Arguments("CalculateTotalPrice")]
        [Arguments("GenerateReport")]
        [Arguments("SendEmailNotification")]
        [Arguments("ValidateUserCredentials")]
        [Arguments("ParseJsonResponse")]
        [Arguments("UpdateDatabaseRecord")]
        [Arguments("ConvertCurrency")]
        [Arguments("FetchWeatherData")]
        [Arguments("ResizeImage")]
        [Arguments("ProcessOrder")]
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

        [Test]
        [Arguments("CalculateTotalPrice")]
        [Arguments("GenerateReport")]
        [Arguments("SendEmailNotification")]
        [Arguments("ValidateUserCredentials")]
        [Arguments("ParseJsonResponse")]
        [Arguments("UpdateDatabaseRecord")]
        [Arguments("ConvertCurrency")]
        [Arguments("FetchWeatherData")]
        [Arguments("ResizeImage")]
        [Arguments("ProcessOrder")]
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

        [Test]
        [Arguments("Calculate-Total-Price")]
        [Arguments("123GenerateReport")]
        [Arguments("Send Email Notification")]
        [Arguments("ValidateUser$Credentials")]
        [Arguments("ParseJson.Response")]
        [Arguments("Update Database Record")]
        [Arguments("convertCurrency")]
        [Arguments("Fetch_Weather_Data")]
        [Arguments("Resize Image")]
        [Arguments("Process@Order")]
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
    }
}