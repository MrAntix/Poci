using System;

namespace Poci.Testing
{
    public class Builder<T>
    {
        Action<T> _assign;
        Func<T> _create = Activator.CreateInstance<T>;

        public Builder<T> CreateWith(
            Func<T> create = null)
        {
            _create = create;

            return this;
        }

        public Builder<T> AssignWith(
            Action<T> assign)
        {
            _assign = assign;

            return this;
        }

        public T Build(
            Action<T> assign = null)
        {
            var i = _create();
            if (_assign != null) _assign(i);
            if (assign != null) assign(i);

            return i;
        }
    }
}