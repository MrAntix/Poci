using Poci.Security.Data;

namespace Examples.AddressBook.InMemory.Data
{
    public class InMemoryUser : IUser
    {
        #region IUser Members

        public bool Active { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        #endregion
    }
}