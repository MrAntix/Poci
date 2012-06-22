using System;
using System.Linq;
using Examples.AddressBook.InMemory.Data;
using Poci.Security.Data;
using Poci.Security.DataServices;

namespace Examples.AddressBook.InMemory.DataService
{
    public class InMemoryUserDataService:
        IUserDataService
    {
        readonly InMemoryDataContext _dataContext;

        public InMemoryUserDataService(InMemoryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IUser GetUser(string email)
        {
            return _dataContext.Users
                .Single(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public bool UserExists(string email)
        {
            return _dataContext.Users
                .Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public IUser CreateUser(string name, string email, string passwordHash)
        {
            return new InMemoryUser
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
    }
}