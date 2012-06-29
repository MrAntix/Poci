using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examples.AddressBook.Data;
using Examples.AddressBook.DataServices;
using Examples.AddressBook.EF.Data;
using Poci.Contacts.Data;
using Poci.Security.Data;

namespace Examples.AddressBook.EF.DataService
{
    public sealed class EFAddressBookContactsDataService :
        IAddressBookContactsDataService
    {
        readonly EFDataContext _dataContext;

        public EFAddressBookContactsDataService(
            EFDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region IAddressBookContactsDataService Members

        bool IAddressBookContactsDataService.
            Exists(IUser user, string email)
        {
            return _dataContext.Users
                .Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        IAddressBookContact IAddressBookContactsDataService.
            Create(IUser user)
        {
            return new EFContact();
        }

        void IAddressBookContactsDataService.
            Insert(IAddressBookContact contact)
        {
            _dataContext.Contacts.Add(contact.Map());
        }

        void IAddressBookContactsDataService.
            Update(IAddressBookContact contact)
        {
        }

        void IAddressBookContactsDataService.
            Delete(IAddressBookContact contact)
        {
            _dataContext.Contacts
                .Remove(Get(contact));
        }

        IEnumerable<IAddressBookContact> IAddressBookContactsDataService.
            ByEmailAddress(string emailAddress)
        {
            return _dataContext.Contacts
                .Where(c => c.Emails.Any(e => e.Address.Equals(emailAddress, StringComparison.OrdinalIgnoreCase))
                );
        }

        IEmail IAddressBookContactsDataService.
            CreateEmail(string address)
        {
            return new EFEmail
                       {
                           Address = address
                       };
        }

        IPhone IAddressBookContactsDataService.
            CreatePhone(string number)
        {
            return new EFPhone
                       {
                           Number = number
                       };
        }

        IAddress IAddressBookContactsDataService.
            CreateAddress(string postcode)
        {
            return new EFAddress
                       {
                           Postcode = postcode
                       };
        }

        public async Task<IEnumerable<IAddressBookContact>>
            Search(string text, string continuationToken, int count)
        {
            return
                _dataContext.Contacts
                    .Where(c =>
                           c.Name.Contains(text)
                           || c.Emails.Any(e => e.Address.Contains(text))
                    );
        }

        #endregion

        EFContact Get(IAddressBookContact contact)
        {
            var id = int.Parse(contact.Identifier);

            return contact is EFContact
                       ? (EFContact) contact
                       : _dataContext.Contacts
                             .Single(c => c.Id == id);
        }
    }
}