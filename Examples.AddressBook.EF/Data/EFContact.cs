using System.Collections.Generic;
using System.Linq;
using Examples.AddressBook.Data;
using Poci.Contacts.Data;
using Poci.Security.Data;

namespace Examples.AddressBook.EF.Data
{
    public class EFContact : EFBase<IAddressBookContact>, IAddressBookContact
    {
        public EFContact()
        {
            This.Emails = new List<IEmail>();
            This.Phones = new List<IPhone>();
            This.Addresses = new List<IAddress>();
        }

        public IEnumerable<EFEmail> Emails
        {
            get { return This.Emails.Cast<EFEmail>(); }
            set { This.Emails = new List<IEmail>(value); }
        }

        public IEnumerable<EFPhone> Phones
        {
            get { return This.Phones.Cast<EFPhone>(); }
            set { This.Phones = new List<IPhone>(value); }
        }

        public IEnumerable<EFAddress> Addresses
        {
            get { return This.Addresses.Cast<EFAddress>(); }
            set { This.Addresses = new List<IAddress>(value); }
        }

        public EFUser Owner
        {
            get { return (EFUser) This.Owner; }
            set { This.Owner = value; }
        }

        #region IAddressBookContact Members

        public string Name { get; set; }
        public string InformalName { get; set; }
        public string Notes { get; set; }
        ICollection<IEmail> IContact.Emails { get; set; }
        ICollection<IPhone> IContact.Phones { get; set; }
        ICollection<IAddress> IContact.Addresses { get; set; }
        IUser IAddressBookContact.Owner { get; set; }

        #endregion
    }
}