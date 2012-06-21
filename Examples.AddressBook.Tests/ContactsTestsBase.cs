using System;
using System.Linq;
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
            using (var contactsService = GetContactsService())
            {
                var contact = contactsService
                    .AddContact(
                        Session,
                        AddressBookContactBuilder.ContactEmail,
                        true);

                Assert.NotNull(contact);
                Assert.NotNull(contact.Emails);
                Assert.Equal(1, contact.Emails.Count);
                Assert.Equal(AddressBookContactBuilder.ContactEmail, contact.Emails.First().Address);
            }
        }

        [Fact]
        public void can_add_a_duplicate_contact_when_allowed()
        {
            using (var contactsService = GetContactsService())
            {
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
            }
        }

        [Fact]
        public void can_not_add_a_duplicate_contact_when_not_allowed()
        {
            using (var contactsService = GetContactsService())
            {
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
            }
        }

        [Fact]
        public void can_list_contacts()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void can_delete_a_contact()
        {
            throw new NotImplementedException();
        }

        protected abstract IAddressBookContactsService GetContactsService();
    }
}