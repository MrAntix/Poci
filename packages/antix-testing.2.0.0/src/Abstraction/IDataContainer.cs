using Testing.Abstraction.Builders;

namespace Testing.Abstraction
{
    public interface IDataContainer
    {
        IDataResources Resources { get; }

    IBooleanBuilder Boolean { get; }
    IIntegerBuilder Integer { get; }
    IDoubleBuilder Double { get; }
    IDateTimeBuilder DateTime { get; }
    ITextBuilder Text { get; }

    IPersonBuilder Person { get; }
    IEmailBuilder Email { get; }
    IWebsiteBuilder Website { get; }
    }
}