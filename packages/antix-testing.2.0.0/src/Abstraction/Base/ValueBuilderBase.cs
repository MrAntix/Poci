using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Testing.Abstraction.Base
{
    public abstract class ValueBuilderBase<TBuilder, T, TLimits> :
        IValueBuilder<TBuilder, T, TLimits>
        where TBuilder : class, IValueBuilder<TBuilder, T, TLimits>
    {
        protected ValueBuilderBase()
        {
        }

        protected ValueBuilderBase(
            TLimits min,
            TLimits max)
        {
            Min = min;
            Max = max;
        }

        protected IEnumerable<T> Items { get; set; }

        public TLimits Max { get; set; }
        public TLimits Min { get; set; }

        public TBuilder WithMax(TLimits max)
        {
            var clone = ClonePrivate();
            clone.Max = max;

            return clone as TBuilder;
        }

        public TBuilder WithRange(TLimits min, TLimits max)
        {
            var clone = ClonePrivate();
            clone.Min = min;
            clone.Max = max;

            return clone as TBuilder;
        }

        public abstract T Build();

        public TBuilder Build(int minCount, int maxCount)
        {
            return Build(
                TestData.Random.Value.Next(minCount, maxCount)
                );
        }

        public TBuilder Build(int exactCount)
        {
            var items =
                Enumerable.Range(0, exactCount)
                    .Select(index => Build());

            if (Items != null) items = Items.Concat(items);

            var clone = ClonePrivate();
            clone.Items = items;

            return clone as TBuilder;
        }

        protected virtual TBuilder CreateClone()
        {
            try
            {
                return Activator.CreateInstance<TBuilder>();
            }
            catch (MissingMethodException mmex)
            {
                throw new MissingMethodException(
                    string.Format("Override the CreateClone method on '{0}', no default contructor found",
                                  typeof (TBuilder).FullName),
                    mmex);
            }
        }

        public TBuilder Clone()
        {
            return ClonePrivate() as TBuilder;
        }

        ValueBuilderBase<TBuilder, T, TLimits> ClonePrivate()
        {
            var clone = CreateClone()
                        as ValueBuilderBase<TBuilder, T, TLimits>;
            Debug.Assert(clone != null, "clone != null");

            clone.Items = Items;
            clone.Min = Min;
            clone.Max = Max;

            return clone;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}