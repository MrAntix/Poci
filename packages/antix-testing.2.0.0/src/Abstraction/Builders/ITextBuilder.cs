using System.Collections.Generic;

namespace Testing.Abstraction.Builders
{
    public interface ITextBuilder :
        IValueBuilder<ITextBuilder, string, int>
    {
        ITextBuilder WithNumbers();
        ITextBuilder WithLetters();
        ITextBuilder WithNumbersAndLetters();
        ITextBuilder WithCharacters(IEnumerable<char> chars);
    }
}