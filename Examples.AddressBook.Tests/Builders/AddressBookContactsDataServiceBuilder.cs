using System;
using System.Collections.Generic;
using System.Linq;
using Examples.AddressBook.Data;
using Examples.AddressBook.DataServices;
using Moq;
using Poci.Security.Data;

namespace Examples.AddressBook.Tests.Builders
{
    public class AddressBookContactsDataServiceBuilder
    {
        public readonly IList<IAddressBookContact> Contacts = new List<IAddressBookContact>();

        public AddressBookContactsDataServiceBuilder WithContact(
            IAddressBookContact value)
        {
            Contacts.Add(value);

            return this;
        }

        public IAddressBookContactsDataService Build()
        {
            var mock = new Mock<IAddressBookContactsDataService>();
            mock.SetupAllProperties();

            mock
                .Setup(x => x.CreateContact(It.IsAny<IUser>()))
                .Returns(
                    (IUser user) => new AddressBookContactBuilder().Build(user)
                );
            mock
                .Setup(x => x.InsertContact(It.IsAny<IAddressBookContact>()))
                .Callback(
                    (IAddressBookContact contact) => Contacts.Add(contact)
                );
            mock
                .Setup(x => x.ContactExists(It.IsAny<IUser>(), It.IsAny<string>()))
                .Returns((
                    IUser owner,
                    string emailAddress
                    )
                         => Contacts.Any(
                             x => x.Owner == owner
                                  && x.Emails
                                         .Any(e => e.Address.Equals(emailAddress, StringComparison.OrdinalIgnoreCase))
                                )
                );
            mock
                .Setup(x => x.CreateEmail(It.IsAny<string>()))
                .Returns(
                    (string emailAddress) => new EmailBuilder().Build(emailAddress)
                );

            return mock.Object;
        }
    }
}