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

        [Test]
        [Arguments("Valid")]
        [Arguments("inValid")]
        public async Task GivenADescriptorOnASealedClassWhenFluentifyIsNotAppliedThenMissingFluentifyRuleIsRaised(string descriptor)
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedMissingFluentifyRule("Property", "TestClass", new LinePosition(4, 5)));

            TestCode = $$"""
                using Fluentify;

                public sealed class TestClass
                {
                    [Descriptor("{{descriptor}}")]
                    public string Property { get; set; }
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Test]
        [Arguments("Valid")]
        public async Task GivenADescriptorOnANonSealedClassWhenFluentifyIsNotAppliedThenNoDiagnosticIsRaised(string descriptor)
        {
            // Arrange
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

        [Test]
        public async Task GivenADuplicateDescriptorWhenFluentifyIsAppliedToTheClassThenDuplicateDescriptorRuleIsRaised()
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedDuplicateDescriptorRule("Update", "SecondProperty", "FirstProperty", "TestClass", new LinePosition(8, 5)));

            TestCode = $$"""
                using Fluentify;

                [Fluentify]
                public class TestClass
                {
                    [Descriptor("Update")]
                    public string FirstProperty { get; set; }

                    [Descriptor("Update")]
                    public string SecondProperty { get; set; }
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Test]
        public async Task GivenAPropertySharingADescriptorWithAnIgnoredPropertyWhenFluentifyIsAppliedToTheClassThenNoDuplicateDescriptorRuleIsRaised()
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedDisregardedRule("FirstProperty", new LinePosition(5, 5)));

            TestCode = $$"""
                using Fluentify;

                [Fluentify]
                public class TestClass
                {
                    [Descriptor("Update")]
                    [Ignore]
                    public string FirstProperty { get; set; }

                    [Descriptor("Update")]
                    public string SecondProperty { get; set; }
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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
    }
}