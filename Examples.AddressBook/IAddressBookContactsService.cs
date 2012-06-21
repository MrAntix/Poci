using System;
using Poci.Contacts.Data;
using Poci.Security.Data;

namespace Examples.AddressBook
{
    public interface IAddressBookContactsService :
        IDisposable
    {
        IContact CreateContact();
        void AddContact(ISession session, IContact contact);
    }
}