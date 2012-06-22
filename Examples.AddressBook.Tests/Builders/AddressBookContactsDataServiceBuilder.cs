using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examples.AddressBook.Data;
using Examples.AddressBook.DataServices;
using Moq;
using Poci.Security.Data;

namespace Examples.AddressBook.Tests.Builders
{
    public class AddressBookContactsDataServiceBuilder
    {
        public readonly List<IAddressBookContact> Contacts = new List<IAddressBookContact>();

        public AddressBookContactsDataServiceBuilder WithContact(
            IAddressBookContact value)
        {
            Contacts.Add(value);

            return this;
        }

        public AddressBookContactsDataServiceBuilder WithContacts(
            IEnumerable<IAddressBookContact> contacts)
        {
            Contacts.AddRange(contacts);

            return this;
        }

        public IAddressBookContactsDataService Build()
        {
            var mock = new Mock<IAddressBookContactsDataService>();
            mock.SetupAllProperties();

            mock
                .Setup(x => x.Create(It.IsAny<IUser>()))
                .Returns(
                    (IUser user) => new AddressBookContactBuilder().Build(user)
                );

            mock
                .Setup(x => x.Insert(It.IsAny<IAddressBookContact>()))
                .Callback(
                    (IAddressBookContact contact) => Contacts.Add(contact)
                );

            mock
                .Setup(x => x.Delete(It.IsAny<IAddressBookContact>()))
                .Callback(
                    (IAddressBookContact contact) => Contacts.Remove(contact)
                );

            mock
                .Setup(x => x.Exists(It.IsAny<IUser>(), It.IsAny<string>()))
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
                    (string emailAddress) =>
                    new EmailBuilder().Build(emailAddress)
                );

            mock
                .Setup(x => x.ByEmailAddress(It.IsAny<string>()))
                .Returns(
                    (string emailAddress) =>
                    Contacts
                        .Where(
                            c => c.Emails.Any(e => e.Address.Equals(emailAddress, StringComparison.OrdinalIgnoreCase))));

            mock
                .Setup(x => x.Search(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(
                    (string text,
                     string continuationToken,
                     int count) =>
                        {
                            int start;
                            int.TryParse(continuationToken, out start);

                            if (string.IsNullOrWhiteSpace(text))
                                return Task.FromResult(
                                    Contacts
                                        .Skip(start).Take(count));

                            return Task.FromResult(
                                Contacts
                                    .Where(c => c.Name.StartsWith(text, StringComparison.OrdinalIgnoreCase))
                                    .Skip(start).Take(count));
                        }
                );

            return mock.Object;
        }
    }
}