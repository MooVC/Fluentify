namespace Fluentify.DescriptorAttributeAnalyzerTests;

using Fluentify.Snippets;
using Microsoft.CodeAnalysis.Text;

public partial class WhenExecuted
{
    public sealed class Class
        : WhenExecuted
    {
        public Class()
            : base(Classes.ReferenceAssemblies, Classes.LanguageVersion)
        {
        }

        [Theory]
        [InlineData("Valid")]
        [InlineData("inValid")]
        public async Task GivenADescriptorWhenFluentifyIsNotAppliedToTheClassThenMissingFluentifyRuleIsRaised(string descriptor)
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedMissingFluentifyRule("TestClass", new LinePosition(4, 5)));

            TestCode = $$"""
                using Fluentify;

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
        public async Task GivenAValidDescriptorWhenFluentifyIsAppliedToTheClassThenNoDiagnosticIsRaised(string descriptor)
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
        [InlineData("convertCurrency")]
        [InlineData("Fetch_Weather_Data")]
        [InlineData("Resize Image")]
        [InlineData("Process@Order")]
        public async Task GivenAnInvalidDescriptorWhenFluentifyIsAppliedToTheClassThenTheValidNamingRuleDiagnosticIsRaised(string descriptor)
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
        public async Task GivenAValidDescriptorOnAnIgnoredPropertyWhenFluentifyIsAppliedToTheClassThenDisregardedRuleIsRaised(string descriptor)
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedDisregardedRule("IgnoredPropertyWithValidDiscriptor", new LinePosition(5, 5)));

            TestCode = $$"""
                using Fluentify;

                [Fluentify]
                public class TestClass
                {
                    [Descriptor("{{descriptor}}")]
                    [Ignore]
                    public string IgnoredPropertyWithValidDiscriptor { get; set; }
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenAValidDescriptorOnABooleanWhenFluentifyIsAppliedToTheClassThenNoDiagnosticIsRaised()
        {
            // Arrange
            TestCode = $$"""
                using Fluentify;

                [Fluentify]
                public class TestClass
                {
                    [Descriptor("IsDifferent")]
                    public bool IsActive { get; set; }
                }
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
                public class TestClass
                {
                    [Descriptor]
                    public string Name { get; set; }
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenARedundantSelfDescriptorOnANonBooleanWhenFluentifyIsAppliedToTheClassThenDisregardedRuleIsRaised()
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedRedundantRule("WithName", "Name", new LinePosition(5, 5)));

            TestCode = $$"""
                using Fluentify;

                [Fluentify]
                public class TestClass
                {
                    [Descriptor("WithName")]
                    public string Name { get; set; }
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenASelfDescriptorOnABooleanWhenFluentifyIsAppliedToTheClassThenDisregardedRuleIsRaised()
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedRedundantRule("IsActive", "IsActive", new LinePosition(5, 5)));

            TestCode = $$"""
                using Fluentify;

                [Fluentify]
                public class TestClass
                {
                    [Descriptor]
                    public bool IsActive { get; set; }
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }
    }
}