using Poci.Contacts.Data;

namespace Examples.AddressBook.EF.Data
{
    public class EFEmail : EFBase<IEmail>, IEmail
    {
        #region IEmail Members

        public string Type { get; set; }
        public string Address { get; set; }

        #endregion
    }
}