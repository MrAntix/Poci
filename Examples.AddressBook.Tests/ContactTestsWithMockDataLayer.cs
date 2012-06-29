using System;
using System.Collections.Generic;
using Examples.AddressBook.Data;
using Examples.AddressBook.Tests.Builders;
using Moq;
using Poci.Common.DataServices;
using Poci.Security;
using Poci.Security.Data;
using Poci.Security.Tests;
using Poci.Security.Tests.Builders;
using Poci.Security.Validation;
using Poci.Testing;

namespace Examples.AddressBook.Tests
{
    public class ContactTestsWithMockDataLayer : ContactsTestsBase<IDataContext>
    {
        protected override IDataContext GetDataContext()
        {
            return Mock.Of<IDataContext>();
        }

        protected override IAddressBookContactsService GetContactsService(
            IDataContext dataContext, ref IEnumerable<IAddressBookContact> contacts)
        {
            var userBuilder = new Builder<IUser>()
                .CreateWith(Mock.Of<IUser>)
                .AssignWith(u =>
                                {
                                    u.Active = true;
                                    u.Email = TestData.User.Email;
                                    u.PasswordHash = HashService.Hash64(TestData.User.CorrectPassword);
                                });

            User = userBuilder.Build();

            Session = new Builder<ISession>()
                .CreateWith(Mock.Of<ISession>)
                .Build(
                    s =>
                        {
                            s.User = User;
                            s.ExpiresOn = DateTime.UtcNow.AddDays(1);
                        });

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