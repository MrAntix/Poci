using System;
using System.Collections.Generic;
using System.Linq;

namespace Examples.AddressBook.EF.Data
{
    public class MappingCollection<TI, TC> :
        List<TC>,
        ICollection<TI>
        where TC : TI
    {
        public MappingCollection()
        {
        }

        public MappingCollection(IEnumerable<TI> items)
        {
            foreach (var item in items) MapAdd(item);
        }

        #region ICollection<TI> Members

        IEnumerator<TI> IEnumerable<TI>.GetEnumerator()
        {
            return this.Cast<TI>().GetEnumerator();
        }

        void ICollection<TI>.Add(TI item)
        {
            MapAdd(item);
        }

        bool ICollection<TI>.Contains(TI item)
        {
            return item is TC && Contains((TC) item);
        }

        void ICollection<TI>.CopyTo(TI[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        bool ICollection<TI>.Remove(TI item)
        {
            return item is TC && Remove((TC) item);
        }

        bool ICollection<TI>.IsReadOnly
        {
            get { return false; }
        }

        #endregion

        void MapAdd(TI item)
        {
            Add(Mapping.Map<TI, TC>(item));
        }
    }
}