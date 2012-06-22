using System.Collections.Generic;
using Examples.AddressBook.Data;
using Poci.Security.Data;

namespace Examples.AddressBook.InMemory.DataService
{
    public class InMemoryDataContext
    {
        public List<IAddressBookContact> Contacts = new List<IAddressBookContact>();
        public List<ISession> Sessions = new List<ISession>();
        public List<IUser> Users = new List<IUser>();
    }
}