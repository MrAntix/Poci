using Poci.Security.Data;

namespace Examples.AddressBook.EF.Data
{
    public class EFUser : EFBase, IUser
    {
        #region IUser Members

        public bool Active { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        #endregion
    }
}