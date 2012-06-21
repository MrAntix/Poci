using Poci.Contacts.Data;
using Poci.Contacts.Data.Services;
using Poci.Security.Data;

namespace Examples.AddressBook
{
    public class AddressBookContactsService : IAddressBookContactsService
    {
        IContactDataService _dataService;

        public AddressBookContactsService(IContactDataService dataService)
        {
            _dataService = dataService;
        }

        public void Dispose()
        {
            
        }

        public IContact CreateContact()
        {
            throw new System.NotImplementedException();
        }

        public void AddContact(ISession session, IContact contact)
        {
            throw new System.NotImplementedException();
        }
    }
}