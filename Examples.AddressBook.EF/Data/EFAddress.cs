using Poci.Contacts.Data;

namespace Examples.AddressBook.EF.Data
{
    public class EFAddress : IAddress
    {
        #region IAddress Members

        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public Countries Country { get; set; }

        #endregion
    }
}