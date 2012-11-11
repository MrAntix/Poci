using System;
using System.Linq;
using Examples.AddressBook.RavenDb.Data;
using Poci.Security.Data;
using Poci.Security.DataServices;

namespace Examples.AddressBook.RavenDb.DataService
{
    public class RavenDbUserDataService :
        IUserDataService
    {
        readonly RavenDbDataContext _dataContext;

        public RavenDbUserDataService(RavenDbDataContext dataContext)
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
            return new RavenDbUser
                       {
                           Name = name,
                           Email = email,
                           PasswordHash = passwordHash
                       };
        }

        public void InsertUser(IUser user)
        {
            _dataContext.Users.Add(user);
        }

        public void UpdateUser(IUser user)
        {
        }

        #endregion
    }
}