using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examples.AddressBook.Data;
using Examples.AddressBook.DataServices;
using Examples.AddressBook.InMemory.Data;
using Poci.Contacts.Data;
using Poci.Security.Data;

namespace Examples.AddressBook.InMemory.DataService
{
    public class InMemoryAddressBookContactsDataService :
        IAddressBookContactsDataService
    {
        readonly InMemoryDataContext _dataContext;

        public InMemoryAddressBookContactsDataService(
            InMemoryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region IAddressBookContactsDataService Members

        public bool Exists(IUser user, string email)
        {
            return _dataContext.Users
                .Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public IAddressBookContact Create(IUser user)
        {
            return new InMemoryAddressBookContact();
        }

        public void Insert(IAddressBookContact contact)
        {
            _dataContext.Contacts.Add(contact);
        }

        public void Update(IAddressBookContact contact)
        {
        }

        public void Delete(IAddressBookContact contact)
        {
            _dataContext.Contacts.Remove(contact);
        }

        public IEnumerable<IAddressBookContact> ByEmailAddress(string emailAddress)
        {
            return _dataContext.Contacts
                .Where(c => c.Emails.Any(e => e.Address.Equals(emailAddress, StringComparison.OrdinalIgnoreCase)));
        }

        public IEmail CreateEmail(string address)
        {
            return new InMemoryEmail
                       {
                           Address = address
                       };
        }

        public IPhone CreatePhone(string number)
        {
            return new InMemoryPhone
                       {
                           Number = number
                       };
        }

        public IAddress CreateAddress(string postcode)
        {
            return new InMemoryAddress
                       {
                           Postcode = postcode
                       };
        }

        public Task<IEnumerable<IAddressBookContact>> Search(string text, string continuationToken, int count)
        {
            return Task.FromResult(
                _dataContext.Contacts
                    .Where(c =>
                           c.Name.StartsWith(text, StringComparison.OrdinalIgnoreCase)
                           || c.Emails.Any(e => e.Address.StartsWith(text, StringComparison.OrdinalIgnoreCase)))
                );
        }

        #endregion
    }
}