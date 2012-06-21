using Poci.Contacts.Data;
using Poci.Security.Data;

namespace Examples.AddressBook.Data
{
    public interface IAddressBookContact : IContact
    {
        IUser Owner { get; set; }
    }
}