using System;
using System.Collections.Generic;
using Examples.AddressBook.Data;
using Poci.Contacts.Data;
using Poci.Security.Data;

namespace Examples.AddressBook.RavenDb.Data
{
    public class RavenDbAddressBookContact : IAddressBookContact
    {
        public RavenDbAddressBookContact()
        {
            Emails = new List<IEmail>();
            Phones = new List<IPhone>();
            Addresses = new List<IAddress>();

            Identifier = Guid.NewGuid().ToString();
        }

        #region IAddressBookContact Members

        public string Name { get; set; }
        public string InformalName { get; set; }
        public string Notes { get; set; }
        public ICollection<IEmail> Emails { get; set; }
        public ICollection<IPhone> Phones { get; set; }
        public ICollection<IAddress> Addresses { get; set; }
        public string Identifier { get; private set; }
        public IUser Owner { get; set; }

        #endregion
    }
}