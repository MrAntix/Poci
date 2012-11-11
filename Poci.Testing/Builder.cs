using System;
using System.Collections.Generic;
using System.Linq;

namespace Poci.Testing
{
    public class Builder<T>
    {
        public IEnumerable<T> Items;
        Action<T> _assign;
        Func<T> _create = Activator.CreateInstance<T>;
        int _index;

        public Builder<T> Create(
            Func<T> create = null)
        {
            _create = create;

            return this;
        }

        public Builder<T> With(
            Action<T> assign)
        {
            _assign = assign;

            return this;
        }

        public T Build(
            Action<T> assign = null)
        {
            var item = _create();
            if (_assign != null) _assign(item);
            if (assign != null) assign(item);

            return item;
        }

        public Builder<T> Build(
            int count,
            Action<T, int> assign = null)
        {
            var items =
                Enumerable.Range(0, count)
                    .Select(index =>
                                {
                                    var item = _create();
                                    if (_assign != null) _assign(item);
                                    if (assign != null) assign(item, _index + index);

                                    return item;
                                });

            if (Items != null) items = Items.Concat(items);

            return new Builder<T>
                       {
                           _assign = _assign,
                           _create = _create,
                           _index = count,
                           Items = items
                       };
        }
    }
}