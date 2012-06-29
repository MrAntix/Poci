using System;
using System.Linq;
using Examples.AddressBook.EF.Data;
using Poci.Security.Data;
using Poci.Security.DataServices;

namespace Examples.AddressBook.EF.DataService
{
    public class EFUserDataService :
        IUserDataService
    {
        readonly EFDataContext _dataContext;

        public EFUserDataService(
            EFDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region IUserDataService Members

        public IUser TryGetUser(string email)
        {
            return _dataContext.Users
                .SingleOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public bool UserExists(string email)
        {
            return _dataContext.Users
                .Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public IUser CreateUser(string name, string email, string passwordHash)
        {
            return new EFUser
                       {
                           Name = name,
                           Email = email,
                           PasswordHash = passwordHash
                       };
        }

        public void InsertUser(IUser user)
        {
        }

        public void UpdateUser(IUser user)
        {
        }

        #endregion
    }
}