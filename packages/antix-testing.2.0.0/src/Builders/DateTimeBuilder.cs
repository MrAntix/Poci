using System;
using Testing.Abstraction.Base;
using Testing.Abstraction.Builders;

namespace Testing.Builders
{
    public class DateTimeBuilder :
        ValueBuilderBase<IDateTimeBuilder, DateTime, DateTime>,
        IDateTimeBuilder
    {
        public DateTimeBuilder() :
            base(DateTime.MinValue, DateTime.MaxValue)
        {
        }

        public override DateTime Build()
        {
            var minTicks = Min.Ticks;
            var maxTicks = Max.Ticks;

            return new DateTime(
                (long) (minTicks + TestData.Random.Value.NextDouble()*(maxTicks - minTicks))
                );
        }

        protected override IDateTimeBuilder CreateClone()
        {
            return new DateTimeBuilder();
        }
    }
}