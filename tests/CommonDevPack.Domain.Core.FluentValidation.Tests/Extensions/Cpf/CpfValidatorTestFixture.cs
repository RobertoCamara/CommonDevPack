using Bogus.Extensions.Brazil;
using CommonDevPack.Domain.Core.FluentValidation.Tests.Extensions.Common;
using Xunit;

namespace CommonDevPack.Domain.Core.FluentValidation.Tests.Extensions.Cpf;


[CollectionDefinition(nameof(CpfValidatorTestFixture))]
public class CpfValidatorTestFixtureCollection
    : ICollectionFixture<CpfValidatorTestFixture>
{ }

public class CpfValidatorTestFixture
    : BaseFixture
{
    public string GetValidCpfNumber()
        => new Bogus.Person().Cpf(false); //TODO: sem criar uma nova instancia de Bogus.Person, o MemberData não funciona para multiplos registros.
}