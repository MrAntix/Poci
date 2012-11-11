using System;
using Testing.Abstraction.Base;

namespace Testing
{
    public class Builder<T>
        : BuilderBase<Builder<T>, T>
    {
        public Builder()
        {
        }

        public Builder(Func<T> create) : base(create)
        {
        }

        protected override Builder<T> CreateClone()
        {
            return new Builder<T>();
        }
    }
}