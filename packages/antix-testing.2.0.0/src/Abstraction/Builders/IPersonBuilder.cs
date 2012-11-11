using Testing.Models;

namespace Testing.Abstraction.Builders
{
    public interface IPersonBuilder :
        IBuilder<IPersonBuilder, PersonModel>
    {
        IPersonBuilder WithAge(int min, int max);
        IPersonBuilder WithGender(GenderTypes gender);
    }
}