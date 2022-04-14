using Bogus.Extensions.Brazil;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CommonDevPack.Domain.Core.FluentValidation.Tests.Extensions.Cpf;

[Collection(nameof(CpfValidatorTestFixture))]
public class CpfValidatorTest
{
    private readonly CpfValidatorTestFixture _fixture;

    public CpfValidatorTest(CpfValidatorTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory(DisplayName = nameof(CpfValidator_Number_Valid))]
    [Trait("Domain.Core", "CpfValidator - FluentValidation")]
    [MemberData(nameof(GetValidCpfNumberData), parameters: 10)]
    public void CpfValidator_Number_Valid(string validCpf)
    {
        var validator = new CpfValidatorFaker();

        var validationResult = validator.Validate(validCpf);

        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().HaveCount(0);
    }

    [Theory(DisplayName = nameof(CpfValidatorError_Number_Invalid))]
    [Trait("Domain.Core", "CpfValidator - FluentValidation")]
    [MemberData(nameof(GetInvalidCpfNumberData), parameters: 5)]
    public void CpfValidatorError_Number_Invalid(string invalidCpf)
    {
        var expected = $"'Cpf' is not a valid cpf number.";

        var validator = new CpfValidatorFaker();

        var validationResult = validator.Validate(invalidCpf);
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(1);
        validationResult
            .Errors[0]
            .ErrorMessage
            .Should()
            .Be(expected);
    }

    [Theory(DisplayName = nameof(CpfValidatorError_Number_EmptyOrWhiteSpace))]
    [Trait("Domain.Core", "CpfValidator - FluentValidation")]
    [InlineData("")]
    [InlineData("   ")]
    public void CpfValidatorError_Number_EmptyOrWhiteSpace(string invalidCpf)
    {
        var expected_0 = "'Cpf' must not be empty.";
        var expected_1 = "'Cpf' is not a valid cpf number.";

        var validator = new CpfValidatorFaker();

        var validationResult = validator.Validate(invalidCpf);
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

    private static IEnumerable<object[]> GetInvalidCpfNumberData(int numberOfTests = 10)
    {
        var fixture = new CpfValidatorTestFixture();
        for (int i = 0; i < numberOfTests; i++)
        {
            yield return new object[] {
                fixture.FakerInstance.Random.Int(11)
            };
        }
    }

    private static IEnumerable<object[]> GetValidCpfNumberData(int numberOfTests = 10)
    {
        var fixture = new CpfValidatorTestFixture();
        for (int i = 0; i < numberOfTests; i++)
        {
            yield return new object[] {
                fixture.GetValidCpfNumber()
            };
        }
    }
}