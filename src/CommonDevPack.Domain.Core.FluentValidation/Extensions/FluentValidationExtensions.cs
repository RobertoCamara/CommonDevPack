using CommonDevPack.Domain.Core.FluentValidation.Validation;
using FluentValidation;

namespace CommonDevPack.Domain.Core.FluentValidation.Extensions;
public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, string> IsValidCpf<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new CpfNumberValidator<T, string>());
    }

    public static IRuleBuilderOptions<T, string> IsValidCnpj<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new CnpjNumberValidator<T, string>());
    }
}
