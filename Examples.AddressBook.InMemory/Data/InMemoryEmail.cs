using Poci.Contacts.Data;

namespace Examples.AddressBook.InMemory.Data
{
    public class InMemoryEmail : IEmail
    {
        #region IEmail Members

        public string Type { get; set; }
        public string Address { get; set; }

        #endregion
    }
}