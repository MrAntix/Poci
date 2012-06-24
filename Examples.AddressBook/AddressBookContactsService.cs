using System.Collections.Generic;
using System.Threading.Tasks;
using Examples.AddressBook.Data;
using Examples.AddressBook.DataServices;
using Examples.AddressBook.Properties;
using Poci.Security;
using Poci.Security.Data;

namespace Examples.AddressBook
{
    public sealed class AddressBookContactsService : IAddressBookContactsService
    {
        readonly IAddressBookContactsDataService _contactDataService;
        readonly ISecurityService _securityService;

        public AddressBookContactsService(
            IAddressBookContactsDataService contactDataService,
            ISecurityService securityService)
        {
            _contactDataService = contactDataService;
            _securityService = securityService;
        }

        #region IAddressBookContactsService Members

        IAddressBookContact IAddressBookContactsService.AddContact(
            ISession session, string emailAddress, bool allowDuplicate)
        {
            _securityService.AssertSessionIsValid(session);

            if (!allowDuplicate
                && _contactDataService.Exists(session.User, emailAddress))
            {
                throw new AddressBookDuplicateContactException();
            }

            var contact = _contactDataService.Create(session.User);
            contact.Emails.Add(
                _contactDataService.CreateEmail(emailAddress)
                );

            _contactDataService.Insert(contact);

            return contact;
        }

        async Task<IEnumerable<IAddressBookContact>> IAddressBookContactsService.Search(
            ISession session, string text, string continuationToken)
        {
            _securityService.AssertSessionIsValid(session);

            return await _contactDataService
                             .Search(text, continuationToken, Settings.Default.SearchCount);
        }

        void IAddressBookContactsService.Delete(
            ISession session, IAddressBookContact contact)
        {
            _securityService.AssertSessionIsValid(session);

            _contactDataService
                .Delete(contact);
        }

        IEnumerable<IAddressBookContact> IAddressBookContactsService.GetByEmail(
            ISession session, string emailAddress)
        {
            _securityService.AssertSessionIsValid(session);

            return _contactDataService
                .ByEmailAddress(emailAddress);
        }

        #endregion
    }
}