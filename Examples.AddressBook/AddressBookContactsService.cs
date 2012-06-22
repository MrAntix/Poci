using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Examples.AddressBook.Data;
using Examples.AddressBook.DataServices;
using Examples.AddressBook.Properties;
using Poci.Security;
using Poci.Security.Data;

namespace Examples.AddressBook
{
    public class AddressBookContactsService : IAddressBookContactsService
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
                && _contactDataService.ContactExists(session.User, emailAddress))
            {
                throw new AddressBookDuplicateContactException();
            }

            var contact = _contactDataService.CreateContact(session.User);
            contact.Emails.Add(
                _contactDataService.CreateEmail(emailAddress)
                );

            _contactDataService.InsertContact(contact);

            return contact;
        }

        async Task<IEnumerable<IAddressBookContact>> IAddressBookContactsService.Search(
            ISession session, string text, string continuationToken)
        {
            _securityService.AssertSessionIsValid(session);

            return await _contactDataService
                .Search(text, continuationToken, Settings.Default.SearchCount);
        }

        #endregion
    }
}