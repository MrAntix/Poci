using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Examples.AddressBook.Data;
using Poci.Contacts.Data;
using Poci.Security.Data;

namespace Examples.AddressBook.EF.Data
{
    public class EFContact : EFBase, IAddressBookContact
    {
        public EFContact()
        {
            Emails = new MappingCollection<IEmail, EFEmail>();
            Phones = new MappingCollection<IPhone, EFPhone>();
            Addresses = new MappingCollection<IAddress, EFAddress>();
        }

        public ICollection<EFEmail> Emails { get; set; }
        public MappingCollection<IPhone, EFPhone> Phones { get; set; }
        public MappingCollection<IAddress, EFAddress> Addresses { get; set; }
        public EFUser Owner { get; set; }

        #region IAddressBookContact Members

        public string Name { get; set; }
        public string InformalName { get; set; }
        public string Notes { get; set; }

        ICollection<IEmail> IContact.Emails
        {
            get { return (MappingCollection<IEmail, EFEmail>) Emails; }
            set { Emails = new MappingCollection<IEmail, EFEmail>(value); }
        }


        ICollection<IPhone> IContact.Phones
        {
            get { return Phones; }
            set { Phones = new MappingCollection<IPhone, EFPhone>(value); }
        }

        ICollection<IAddress> IContact.Addresses
        {
            get { return Addresses; }
            set { Addresses = new MappingCollection<IAddress, EFAddress>(value); }
        }

        [NotMapped]
        public string Identifier
        {
            get { return Id.ToString(CultureInfo.InvariantCulture); }
        }

        IUser IAddressBookContact.Owner
        {
            get { return Owner; }
            set { Owner = value.Map(); }
        }

        #endregion
    }
}