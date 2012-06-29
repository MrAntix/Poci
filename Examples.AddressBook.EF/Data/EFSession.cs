using System;
using Poci.Security.Data;

namespace Examples.AddressBook.EF.Data
{
    public class EFSession : EFBase, ISession
    {
        public EFUser User { get; set; }

        #region ISession Members

        public Guid Identifier { get; set; }

        IUser ISession.User
        {
            get { return User; }
            set { User = value.Map(); }
        }

        public DateTime CreatedOn { get; set; }
        public DateTime ExpiresOn { get; set; }

        #endregion
    }
}