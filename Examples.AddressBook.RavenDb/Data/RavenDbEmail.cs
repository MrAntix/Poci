using Poci.Contacts.Data;

namespace Examples.AddressBook.RavenDb.Data
{
    public class RavenDbEmail : IEmail
    {
        #region IEmail Members

        public string Type { get; set; }
        public string Address { get; set; }

        #endregion
    }
}