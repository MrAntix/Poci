using System;
using System.Collections.Generic;
using Examples.AddressBook.Data;
using Examples.AddressBook.InMemory.Data;
using Examples.AddressBook.InMemory.DataService;
using Poci.Security;
using Poci.Security.Tests;
using Poci.Security.Validation;

namespace Examples.AddressBook.Tests
{
    public class ContactTestsWithInMemoryDataLayer :
        ContactsTestsBase<InMemoryDataContext>
    {
        protected override InMemoryDataContext GetDataContext()
        {
            return new InMemoryDataContext();
        }

        protected override IAddressBookContactsService GetContactsService(
            InMemoryDataContext dataContext, ref IEnumerable<IAddressBookContact> contacts)
        {
            if (contacts != null) dataContext.Contacts.AddRange(contacts);
            contacts = dataContext.Contacts;

            dataContext.Users.Add(
                User = new InMemoryUser
                           {
                               Active = true,
                               Email = TestData.User.Email,
                               PasswordHash = HashService.Hash64(TestData.User.CorrectPassword)
                           }
                );

            dataContext.Sessions.Add(
                Session = new InMemorySession
                              {
                                  User = User,
                                  ExpiresOn = DateTime.UtcNow.AddDays(1)
                              });

            return new AddressBookContactsService(
                new InMemoryAddressBookContactsDataService(dataContext),
                new SecurityService(
                    new InMemoryUserDataService(dataContext),
                    new InMemorySessionDataService(dataContext),
                    HashService,
                    new UserRegistrationValidator()
                    )
                );
        }
    }
}