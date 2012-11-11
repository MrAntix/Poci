using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Examples.AddressBook.Data;
using Examples.AddressBook.Tests.Builders;
using Poci.Common.DataServices;
using Poci.Common.Security;
using Poci.Security.Data;
using Xunit;

namespace Examples.AddressBook.Tests
{
    public abstract class ContactsTestsBase<TDataContext> :
        IDisposable
        where TDataContext : IDataContext
    {
        protected readonly IHashService HashService = new MD5HashService();
        protected ISession Session;
        protected IUser User;

        #region IDisposable Members

        /// <summary>
        ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
        public void Dispose()
        {
            HashService.Dispose();
        }

        #endregion

        [Fact]
        public void can_add_a_new_contact()
        {
            using (var dataContext = GetDataContext())
            {
                IEnumerable<IAddressBookContact> contacts = null;
                var contactsService = GetContactsService(dataContext, ref contacts);

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
        }

        [Fact]
        public void can_add_a_duplicate_contact_when_allowed()
        {
            using (var dataContext = GetDataContext())
            {
                IEnumerable<IAddressBookContact> contacts = null;
                var contactsService = GetContactsService(dataContext, ref contacts);

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
        }

        [Fact]
        public void can_not_add_a_duplicate_contact_when_not_allowed()
        {
            using (var dataContext = GetDataContext())
            {
                IEnumerable<IAddressBookContact> contacts = null;
                var contactsService = GetContactsService(dataContext, ref contacts);

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
        }

        [Fact]
        public async Task can_list_contacts()
        {
            using (var dataContext = GetDataContext())
            {
                var contacts = (IEnumerable<IAddressBookContact>)
                               new[]
                                   {
                                       new AddressBookContactBuilder()
                                           .WithName("One").WithEmail("one@example.com")
                                           .Build(User),
                                       new AddressBookContactBuilder()
                                           .WithName("Two").WithEmail("two@example.com")
                                           .Build(User)
                                   };

                var contactsService = GetContactsService(dataContext, ref contacts);

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
        }

        [Fact]
        public void get_contacts_by_email()
        {
            using (var dataContext = GetDataContext())
            {
                const string sameEmailAddress = "same@example.com";

                var contacts = (IEnumerable<IAddressBookContact>)
                               new[]
                                   {
                                       new AddressBookContactBuilder()
                                           .WithName("One").WithEmail("one@example.com")
                                           .Build(User),
                                       new AddressBookContactBuilder()
                                           .WithName("Two").WithEmail(sameEmailAddress)
                                           .Build(User),
                                       new AddressBookContactBuilder()
                                           .WithName("Three").WithEmail(sameEmailAddress)
                                           .Build(User)
                                   };

                var contactsService = GetContactsService(dataContext, ref contacts);

                var results =
                    contactsService
                        .GetByEmail(
                            Session,
                            sameEmailAddress);

                Assert.NotNull(results);
                Assert.Equal(2, results.Count());
            }
        }

        [Fact]
        public void can_delete_a_contact()
        {
            using (var dataContext = GetDataContext())
            {
                var builder = new AddressBookContactBuilder();

                var contactToDelete = builder
                    .WithName("One").WithEmail("one@example.com")
                    .Build(User);
                var contacts = (IEnumerable<IAddressBookContact>)
                               new[]
                                   {
                                       contactToDelete,
                                       builder.WithName("Two").WithEmail("two@example.com").
                                           Build(User)
                                   };

                var contactsService = GetContactsService(dataContext, ref contacts);

                contactsService.Delete(Session, contactToDelete);

                Assert.Equal(1, contacts.Count());
            }
        }

        protected abstract TDataContext GetDataContext();

        protected abstract IAddressBookContactsService GetContactsService(
            TDataContext dataContext, ref IEnumerable<IAddressBookContact> contacts);
    }
}