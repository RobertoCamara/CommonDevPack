using CommonDevPack.Domain.Core.FluentValidation.Extensions;
using FluentValidation;

namespace CommonDevPack.Domain.Core.FluentValidation.Tests.Extensions.Cnpj;
public class CnpjValidatorFaker : AbstractValidator<string>
{
    public CnpjValidatorFaker()
    {
        RuleFor(x => x)
            .NotEmpty()
            .IsValidCnpj()
            .WithName("Cnpj");
    }
}