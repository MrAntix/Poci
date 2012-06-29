using System;
using System.Collections.Generic;
using Examples.AddressBook.Data;
using Examples.AddressBook.EF.Data;
using Examples.AddressBook.EF.DataService;
using Poci.Common.DataServices;
using Poci.Security;
using Poci.Security.Tests.Builders;
using Poci.Security.Validation;

namespace Examples.AddressBook.Tests
{
    public class ContactTestsWithEFDataLayer : ContactsTestsBase<EFDataContext>
    {
        protected override EFDataContext GetDataContext()
        {
            return new EFDataContext();
        }

        protected override IAddressBookContactsService GetContactsService(
            EFDataContext dataContext, 
            ref IEnumerable<IAddressBookContact> contacts)
        {
            if (contacts != null)
                foreach (var contact in contacts)
                    dataContext.Contacts.Add(contact.Map());

            contacts = dataContext.Contacts;

            var user = new EFUser
                           {
                               Email = UserBuilder.UserEmail,
                               PasswordHash = HashService.Hash64(UserBuilder.CorrectPassword)
                           };
            User = user;
            dataContext.Users.Add(user);

            var session = new EFSession
                              {
                                  Identifier = Guid.NewGuid(),
                                  User = user,
                                  CreatedOn = DateTime.UtcNow,
                                  ExpiresOn = DateTime.UtcNow.AddDays(1)
                              };
            Session = session;
            dataContext.Sessions.Add(session);

            dataContext.CommitAsync().Wait();

            return new AddressBookContactsService(
                new EFAddressBookContactsDataService(dataContext),
                new SecurityService(
                    new EFUserDataService(dataContext),
                    new EFSessionDataService(dataContext),
                    HashService,
                    new UserRegistrationValidator()
                    )
                );
        }
    }
}