using System;
using Poci.Security.Data;

namespace Examples.AddressBook.EF.Data
{
    public class EFSession : EFBase<ISession>, ISession
    {
        public EFUser User
        {
            get { return (EFUser) This.User; }
            set { This.User = value; }
        }

        #region ISession Members

        public Guid Identifier { get; set; }
        IUser ISession.User { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiresOn { get; set; }

        #endregion
    }
}