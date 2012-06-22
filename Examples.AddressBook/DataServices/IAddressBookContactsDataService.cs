using System.Collections.Generic;
using System.Threading.Tasks;
using Examples.AddressBook.Data;
using Poci.Contacts.Data;
using Poci.Security.Data;

namespace Examples.AddressBook.DataServices
{
    public interface IAddressBookContactsDataService
    {
        IAddressBookContact CreateContact(IUser user);
        void InsertContact(IAddressBookContact contact);
        void UpdateContact(IAddressBookContact contact);
        bool ContactExists(IUser user, string email);
        IEmail CreateEmail(string emailAddress);

        Task<IEnumerable<IAddressBookContact>> Search(
            string text, string continuationToken, int count);
    }
}