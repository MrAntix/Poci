using System;
using Poci.Security.Data;

namespace Examples.AddressBook.RavenDb.Data
{
    public class RavenDbSession : ISession
    {
        #region ISession Members

        public Guid Identifier { get; set; }
        public IUser User { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiresOn { get; set; }

        #endregion
    }
}