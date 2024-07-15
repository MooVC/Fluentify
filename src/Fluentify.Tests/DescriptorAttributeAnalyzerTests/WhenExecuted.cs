namespace Fluentify.DescriptorAttributeAnalyzerTests;

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;

public sealed class WhenExecuted
    : AnalyzerTests<DescriptorAttributeAnalyzer, DescriptorAttributeGenerator>
{
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

    protected override IEnumerable<Type> GetSourceGenerators()
    {
        return [typeof(DescriptorAttributeGenerator), typeof(FluentifyAttributeGenerator), typeof(IgnoreAttributeGenerator)];
    }

    private static DiagnosticResult GetExpectedDisregardedRule(string member, LinePosition position)
    {
        return GetExpected(member, position, DescriptorAttributeAnalyzer.DisregardedRule);
    }

    private static DiagnosticResult GetExpectedMissingFluentifyRule(string @class, LinePosition position)
    {
        return GetExpected(@class, position, DescriptorAttributeAnalyzer.MissingFluentifyRule);
    }

    private static DiagnosticResult GetExpectedValidNamingRule(string descriptor, LinePosition position)
    {
        return GetExpected(descriptor, position, DescriptorAttributeAnalyzer.ValidNamingRule);
    }

    private static DiagnosticResult GetExpected(string argument, LinePosition position, DiagnosticDescriptor rule)
    {
        return new DiagnosticResult(rule)
            .WithArguments(argument)
            .WithLocation(position);
    }
}