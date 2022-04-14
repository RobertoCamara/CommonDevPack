using CommonDevPack.Domain.Core.FluentValidation.Extensions;
using FluentValidation;

namespace CommonDevPack.Domain.Core.FluentValidation.Tests.Extensions.Cpf;
public class CpfValidatorFaker : AbstractValidator<string>
{
    public CpfValidatorFaker()
    {
        RuleFor(x => x)
            .NotEmpty()
            .IsValidCpf()
            .WithName("Cpf");
    }
}