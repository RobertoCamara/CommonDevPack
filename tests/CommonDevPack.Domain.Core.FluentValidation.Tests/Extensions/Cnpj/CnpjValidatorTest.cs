using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace CommonDevPack.Domain.Core.FluentValidation.Tests.Extensions.Cnpj;


[Collection(nameof(CnpjValidatorTestFixture))]
public class CnpjValidatorTest
{
    private readonly CnpjValidatorTestFixture _fixture;

    public CnpjValidatorTest(CnpjValidatorTestFixture fixture)
        => _fixture = fixture;

    [Theory(DisplayName = nameof(CnpjValidator_Number_Valid))]
    [Trait("Domain.Core", "CnpjValidator - FluentValidation")]
    [MemberData(nameof(GetValidCnpjNumber), parameters: 10)]
    public void CnpjValidator_Number_Valid(string validCnpj)
    {
        var validator = new CnpjValidatorFaker();
        
        var validationResult = validator.Validate(validCnpj);
        
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().HaveCount(0);
    }

    [Theory(DisplayName = nameof(CnpjValidatorError_Number_Invalid))]
    [Trait("Domain.Core", "CnpjValidator - FluentValidation")]
    [MemberData(nameof(GetInvalidCnpjNumber), parameters: 10)]
    public void CnpjValidatorError_Number_Invalid(string invalidCnpj)
    {
        var expected = $"'Cnpj' is not a valid cnpj number.";

        var validator = new CnpjValidatorFaker();

        var validationResult = validator.Validate(invalidCnpj);
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(1);
        validationResult
            .Errors[0]
            .ErrorMessage
            .Should()
            .Be(expected);
    }

    [Theory(DisplayName = nameof(CnpjValidatorError_Number_EmptyOrWhiteSpace))]
    [Trait("Domain.Core", "CnpjValidator - FluentValidation")]
    [InlineData("")]
    [InlineData("   ")]
    public void CnpjValidatorError_Number_EmptyOrWhiteSpace(string invalidCnpj)
    {
        var expected_0 = "'Cnpj' must not be empty.";
        var expected_1 = "'Cnpj' is not a valid cnpj number.";

        var validator = new CnpjValidatorFaker();

        var validationResult = validator.Validate(invalidCnpj);
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(2);
        validationResult
            .Errors[0]
            .ErrorMessage
            .Should()
            .Be(expected_0);

        validationResult
            .Errors[1]
            .ErrorMessage
            .Should()
            .Be(expected_1);

    }

    private static IEnumerable<object[]> GetValidCnpjNumber(int numberOfTests = 10)
    {
        var fixture = new CnpjValidatorTestFixture();
        for (int i = 0; i < numberOfTests; i++)
        {
            yield return new object[] {
                fixture.GetValidCnpjNumber()
            };
        }
    }

    private static IEnumerable<object[]> GetInvalidCnpjNumber(int numberOfTests = 10)
    {
        var fixture = new CnpjValidatorTestFixture();
        for (int i = 0; i < numberOfTests; i++)
        {
            yield return new object[] {
                fixture.FakerInstance.Random.String2(14)
            };
        }
    }    
}