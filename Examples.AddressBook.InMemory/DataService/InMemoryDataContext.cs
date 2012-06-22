using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Examples.AddressBook.Data;
using Poci.Common.DataServices;
using Poci.Security.Data;

namespace Examples.AddressBook.InMemory.DataService
{
    public class InMemoryDataContext : IDataContext
    {
        public List<IAddressBookContact> Contacts = new List<IAddressBookContact>();
        public List<ISession> Sessions = new List<ISession>();
        public List<IUser> Users = new List<IUser>();

        #region IDataContext Members

        void IDisposable.Dispose()
        {
        }

        async Task IDataContext.CommitAsync()
        {
        }

        #endregion
    }
}