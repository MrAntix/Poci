using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examples.AddressBook.Data;
using Examples.AddressBook.Tests.Builders;
using Poci.Common.Security;
using Poci.Security.Data;
using Xunit;

namespace Examples.AddressBook.Tests
{
    public abstract class ContactsTestsBase
    {
        protected readonly IHashService HashService = new MD5HashService();
        protected ISession Session;
        protected IUser User;

        [Fact]
        public void can_add_a_new_contact()
        {
            IEnumerable<IAddressBookContact> contacts = null;
            var contactsService = GetContactsService(ref contacts);

            var contact = contactsService
                .AddContact(
                    Session,
                    AddressBookContactBuilder.ContactEmail,
                    true);

            Assert.NotNull(contact);
            Assert.NotNull(contact.Emails);
            Assert.Equal(1, contact.Emails.Count);
            Assert.Equal(AddressBookContactBuilder.ContactEmail, contact.Emails.First().Address);

            Assert.NotNull(contacts);
            Assert.Equal(1, contacts.Count());
        }

        [Fact]
        public void can_add_a_duplicate_contact_when_allowed()
        {
            IEnumerable<IAddressBookContact> contacts = null;
            var contactsService = GetContactsService(ref contacts);

            contactsService
                .AddContact(
                    Session,
                    AddressBookContactBuilder.ContactEmail,
                    true);

            Assert.DoesNotThrow(
                () =>
                contactsService
                    .AddContact(
                        Session,
                        AddressBookContactBuilder.ContactEmail,
                        true)
                );

            Assert.NotNull(contacts);
            Assert.Equal(2, contacts.Count());
        }

        [Fact]
        public void can_not_add_a_duplicate_contact_when_not_allowed()
        {
            IEnumerable<IAddressBookContact> contacts = null;
            var contactsService = GetContactsService(ref contacts);

            contactsService
                .AddContact(
                    Session,
                    AddressBookContactBuilder.ContactEmail,
                    true);

            Assert.Throws<AddressBookDuplicateContactException>(
                () =>
                contactsService
                    .AddContact(
                        Session,
                        AddressBookContactBuilder.ContactEmail,
                        false)
                );

            Assert.NotNull(contacts);
            Assert.Equal(1, contacts.Count());
        }

        [Fact]
        public async Task can_list_contacts()
        {
            var builder = new AddressBookContactBuilder();
            IEnumerable<IAddressBookContact> contacts = new[]
                               {
                                   builder.WithName("One").WithEmail("one@example.com").Build(User),
                                   builder.WithName("Two").WithEmail("two@example.com").Build(User)
                               };

            var contactsService = GetContactsService(ref contacts);

            var text = string.Empty;
            var continuationToken = string.Empty;
            var searchResult =
                await contactsService
                          .Search(
                              Session,
                              text,
                              continuationToken);

            Assert.NotNull(searchResult);
            Assert.Equal(contacts.Count(), searchResult.Count());
        }

        [Fact]
        public void can_delete_a_contact()
        {
            throw new NotImplementedException();
        }

        protected IAddressBookContactsService GetContactsService()
        {
            IEnumerable<IAddressBookContact> contacts = null;
            return GetContactsService(ref contacts);
        }

        protected abstract IAddressBookContactsService GetContactsService(
            ref IEnumerable<IAddressBookContact> contacts);
    }
}