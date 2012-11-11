using Testing.Models;

namespace Testing.Abstraction.Builders
{
    public interface IEmailBuilder :
        IBuilder<IEmailBuilder, EmailModel>
    {
        IEmailBuilder WithPerson(PersonModel person);
    }
}