using System.Collections.Generic;
using System.Threading.Tasks;
using Examples.AddressBook.Data;
using Poci.Contacts.Data;
using Poci.Security.Data;

namespace Examples.AddressBook.DataServices
{
    public interface IAddressBookContactsDataService
    {
        bool Exists(IUser user, string email);

        IAddressBookContact Create(IUser user);
        void Insert(IAddressBookContact contact);
        void Update(IAddressBookContact contact);
        void Delete(IAddressBookContact contact);

        IEnumerable<IAddressBookContact> ByEmailAddress(string emailAddress);

        IEmail CreateEmail(string emailAddress);

        Task<IEnumerable<IAddressBookContact>> Search(
            string text, string continuationToken, int count);
    }
}