using Bogus.Extensions.Brazil;
using CommonDevPack.Domain.Core.FluentValidation.Tests.Extensions.Common;
using Xunit;

namespace CommonDevPack.Domain.Core.FluentValidation.Tests.Extensions.Cnpj;

[CollectionDefinition(nameof(CnpjValidatorTestFixture))]
public class CnpjValidatorTestFixtureCollection
    : ICollectionFixture<CnpjValidatorTestFixture>
{}

public class CnpjValidatorTestFixture
    : BaseFixture
{
    public string GetValidCnpjNumber()
        => FakerInstance.Company
                .Cnpj(false);

}