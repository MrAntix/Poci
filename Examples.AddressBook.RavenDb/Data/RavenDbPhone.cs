using Poci.Contacts.Data;

namespace Examples.AddressBook.RavenDb.Data
{
    public class RavenDbPhone : IPhone
    {
        #region IPhone Members

        public string Type { get; set; }
        public Countries Country { get; set; }
        public string Number { get; set; }
        public string Extension { get; set; }

        #endregion
    }
}