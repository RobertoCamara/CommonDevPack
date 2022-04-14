using Bogus;

namespace CommonDevPack.Domain.Core.FluentValidation.Tests.Extensions.Common;
public abstract class BaseFixture
{
    public Faker FakerInstance { get; private set; }

    protected BaseFixture(string locale = "pt_BR")
        => FakerInstance = new Faker(locale);
}