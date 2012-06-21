using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poci.Contacts.Data.Services;
using Poci.Contacts.Tests.Builders;
using Poci.Security.Tests.Builders;

namespace Examples.AddressBook.Tests
{
    [TestClass]
    public class contact_test
    {
        [TestMethod]
        public void can_add_a_new_contact()
        {
            var session = new SessionBuilder()
                .WithUser(
                    new UserBuilder()
                        .BuildUser(UserBuilder.UserEmail)
                )
                .Build();

            var contactDataService = new ContactDataServiceBuilder()
                .Build();

            using (var contactsService = GetContactsService(contactDataService))
            {
                var contact = contactsService.CreateContact();
                contactsService.AddContact(session, contact);
            }
        }

        [TestMethod]
        public void can_list_contacts()
        {
        }

        [TestMethod]
        public void can_delete_a_contact()
        {
        }

        IAddressBookContactsService GetContactsService(IContactDataService dataService)
        {
            return new AddressBookContactsService(dataService);
        }
    }
}