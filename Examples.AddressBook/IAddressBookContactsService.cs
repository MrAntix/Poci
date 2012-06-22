using System.Collections.Generic;
using System.Threading.Tasks;
using Examples.AddressBook.Data;
using Poci.Security.Data;

namespace Examples.AddressBook
{
    public interface IAddressBookContactsService
    {
        IAddressBookContact AddContact(
            ISession session, string emailAddress, bool allowDuplicate = false);

        Task<IEnumerable<IAddressBookContact>> Search(
            ISession session, string text, string continuationToken = null);

        void Delete(
            ISession session, IAddressBookContact contact);

        IEnumerable<IAddressBookContact> GetByEmail(
            ISession session, string emailAddress);
    }
}