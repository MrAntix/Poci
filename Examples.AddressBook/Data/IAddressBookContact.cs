using Poci.Contacts.Data;
using Poci.Security.Data;

namespace Examples.AddressBook.Data
{
    public interface IAddressBookContact : IContact
    {
        string Identifier { get; }
        IUser Owner { get; set; }
    }
}