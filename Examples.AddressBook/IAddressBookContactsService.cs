using System;
using Examples.AddressBook.Data;
using Poci.Security.Data;

namespace Examples.AddressBook
{
    public interface IAddressBookContactsService :
        IDisposable
    {
        IAddressBookContact AddContact(
            ISession session, string emailAddress, bool allowDuplicate);
    }
}