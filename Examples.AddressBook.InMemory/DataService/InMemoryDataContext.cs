using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Examples.AddressBook.Data;
using Poci.Common.DataServices;
using Poci.Security.Data;

namespace Examples.AddressBook.InMemory.DataService
{
    public sealed class InMemoryDataContext : IDataContext
    {
        public List<IAddressBookContact> Contacts = new List<IAddressBookContact>();
        public List<ISession> Sessions = new List<ISession>();
        public List<IUser> Users = new List<IUser>();

        #region IDataContext Members

#pragma warning disable 1998
        async Task IDataContext.CommitAsync()
#pragma warning restore 1998
        {
        }

        #endregion

        #region IDisposable

        bool _disposed;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // Dispose managed resources.
            }

            // unmanaged resources here.

            _disposed = true;
        }

        #endregion
    }
}