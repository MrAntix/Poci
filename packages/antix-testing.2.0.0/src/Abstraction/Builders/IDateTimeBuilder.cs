using System;

namespace Testing.Abstraction.Builders
{
    public interface IDateTimeBuilder :
        IValueBuilder<IDateTimeBuilder, DateTime, DateTime>
    {
    }
}