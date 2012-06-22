using System;
using System.Collections.Generic;
using Examples.AddressBook.Data;
using Examples.AddressBook.InMemory.Data;
using Examples.AddressBook.InMemory.DataService;
using Poci.Security;
using Poci.Security.Tests.Builders;
using Poci.Security.Validation;

namespace Examples.AddressBook.Tests
{
    public class contact_tests_with_in_memory_data_layer : ContactsTestsBase
    {
        protected override IAddressBookContactsService GetContactsService(
            ref IEnumerable<IAddressBookContact> contacts)
        {
            var dataContext = new InMemoryDataContext();
            if (contacts != null) dataContext.Contacts.AddRange(contacts);
            contacts = dataContext.Contacts;

            dataContext.Users.Add(
                User = new InMemoryUser
                           {
                               Email = UserBuilder.UserEmail,
                               PasswordHash = HashService.Hash64(UserBuilder.CorrectPassword)
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