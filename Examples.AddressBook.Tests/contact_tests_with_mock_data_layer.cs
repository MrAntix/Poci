using System;
using System.Collections.Generic;
using Examples.AddressBook.Data;
using Examples.AddressBook.Tests.Builders;
using Poci.Security;
using Poci.Security.Tests.Builders;
using Poci.Security.Validation;

namespace Examples.AddressBook.Tests
{
    public class contact_tests_with_mock_data_layer : ContactsTestsBase
    {
        protected override IAddressBookContactsService GetContactsService(
            ref IEnumerable<IAddressBookContact> contacts)
        {
            var userBuilder = new UserBuilder();

            User = userBuilder
                .Build(
                    UserBuilder.UserEmail,
                    HashService.Hash64(UserBuilder.CorrectPassword));

            Session = new SessionBuilder()
                .WithUser(User)
                .WithExpiresOn(DateTime.UtcNow.AddDays(1))
                .Build();

            var dataServiceBuider = new AddressBookContactsDataServiceBuilder();
            if (contacts != null) dataServiceBuider.WithContacts(contacts);
            contacts = dataServiceBuider.Contacts;

            return new AddressBookContactsService(
                dataServiceBuider.Build(),
                new SecurityService(
                    new UserDataServiceBuilder()
                        .WithUser(User)
                        .Build(),
                    new SessionDataServiceBuilder()
                        .WithSession(Session)
                        .Build(),
                    HashService,
                    new UserRegistrationValidator()
                    )
                );
        }
    }
}