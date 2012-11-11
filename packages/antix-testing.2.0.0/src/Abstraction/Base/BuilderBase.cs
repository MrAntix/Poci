using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Testing.Abstraction.Base
{
    public abstract class BuilderBase<TBuilder, T> :
        IBuilder<TBuilder, T>
        where TBuilder : class, IBuilder<TBuilder, T>
    {
        protected Action<T> Assign;
        protected int Index;
        Func<T> _create = Activator.CreateInstance<T>;

        protected BuilderBase()
        {
        }

        protected BuilderBase(Func<T> create)
        {
            _create = create;
        }

        protected IEnumerable<T> Items { get; set; }

        #region ICloneable Members

        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

        public TBuilder Clone()
        {
            return ClonePrivate() as TBuilder;
        }

        BuilderBase<TBuilder, T> ClonePrivate()
        {
            var clone = CreateClone()
                        as BuilderBase<TBuilder, T>;
            Debug.Assert(clone != null, "clone != null");

            clone._create = _create;
            clone.Assign = Assign;
            clone.Index = Index;
            clone.Items = Items;

            return clone;
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

        public TBuilder With(
            Action<T> assign)
        {
            if (assign == null) throw new ArgumentNullException("assign");

            var clone = ClonePrivate();
            clone.Assign = Assign == null
                               ? assign
                               : x =>
                                     {
                                         Assign(x);
                                         assign(x);
                                     };

            return clone as TBuilder;
        }

        public T Build()
        {
            return Build(null);
        }

        public T Build(Action<T> assign)
        {
            var item = _create();
            if (Assign != null) Assign(item);
            if (assign != null) assign(item);

            return item;
        }

        public TBuilder Build(
            int minCount, int maxCount)
        {
            return Build(minCount, maxCount, default(Action<T>));
        }

        public TBuilder Build(
            int minCount, int maxCount,
            Action<T> assign)
        {
            return Build(TestData.Random.Value.Next(minCount, maxCount), assign);
        }

        public TBuilder Build(
            int minCount, int maxCount,
            Action<T, int> assign)
        {
            return Build(TestData.Random.Value.Next(minCount, maxCount), assign);
        }

        public TBuilder Build(
            int exactCount)
        {
            return Build(exactCount, default(Action<T, int>));
        }

        public TBuilder Build(
            int exactCount,
            Action<T> assign)
        {

            return Build(exactCount,
                         assign == null
                             ? default(Action<T, int>)
                             : (x, i) => assign(x));
        }

        public TBuilder Build(
            int exactCount,
            Action<T, int> assign)
        {
            var items =
                Enumerable.Range(0, exactCount)
                    .Select(index =>
                    {
                        var item = Build();
                        if (assign != null) assign(item, Index + index);

                        return item;
                    });

            if (Items != null) items = Items.Concat(items);

            var clone = ClonePrivate();
            clone.Index = exactCount;
            clone.Items = items;

            return clone as TBuilder;
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