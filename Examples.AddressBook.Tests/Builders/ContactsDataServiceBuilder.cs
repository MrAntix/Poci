using System.Collections.Generic;
using Examples.AddressBook.DataServices;
using Moq;
using Poci.Contacts.Data;
using Poci.Security.Data;

namespace Examples.AddressBook.Tests.Builders
{
    public class ContactsDataServiceBuilder
    {
        public readonly IList<IContact> Contacts = new List<IContact>();

        public ContactsDataServiceBuilder WithContact(
            IContact value)
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
                .Setup(x => x.CreateEmail(It.IsAny<string>()))
                .Returns(
                    (string emailAddress) => new EmailBuilder().Build(emailAddress)
                );

            return mock.Object;
        }
    }
}