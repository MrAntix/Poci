using Testing.Abstraction.Base;
using Testing.Abstraction.Builders;

namespace Testing.Builders
{
    public class IntegerBuilder :
        ValueBuilderBase<IIntegerBuilder, int, int>,
        IIntegerBuilder
    {
        public IntegerBuilder() :
            base(0, int.MaxValue)
        {
        }

        public override int Build()
        {
            return TestData.Random.Value.Next(Min, Max);
        }

        protected override IIntegerBuilder CreateClone()
        {
            return new IntegerBuilder();
        }
    }
}