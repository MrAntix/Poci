using System;
using Poci.Security.Data;

namespace Examples.AddressBook.InMemory.Data
{
    public class InMemorySession : ISession
    {
        #region ISession Members

        public Guid Identifier { get; set; }
        public IUser User { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiresOn { get; set; }

        #endregion
    }
}