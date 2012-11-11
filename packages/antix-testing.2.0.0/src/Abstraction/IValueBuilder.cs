using System;
using System.Collections.Generic;

namespace Testing.Abstraction
{
    public interface IValueBuilder<TBuilder, T, TLimits> :
        IEnumerable<T>, ICloneable
        where TBuilder : class, IValueBuilder<TBuilder, T, TLimits>
    {
        TLimits Min { get; }
        TLimits Max { get; }

        TBuilder WithMax(TLimits max);
        TBuilder WithRange(TLimits min, TLimits max);
        T Build();
        TBuilder Build(int minCount, int maxCount);
        TBuilder Build(int exactCount);
    }
}