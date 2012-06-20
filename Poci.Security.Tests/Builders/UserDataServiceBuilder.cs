using System;
using System.Collections.Generic;
using System.Linq;
using Common.Security;
using Moq;
using Poci.Security.Data;
using Poci.Security.Data.Services;

namespace Poci.Security.Tests
{
    internal class UserDataServiceBuilder
    {
        readonly IHashService _hashService;
        readonly IList<IUser> _users = new List<IUser>();

        public UserDataServiceBuilder(
            IHashService hashService)
        {
            _hashService = hashService;
        }

        public UserDataServiceBuilder WithUser(
            IUser user)
        {
            _users.Add(user);

            return this;
        }

        public IUserDataService Build()
        {
            var mock = new Mock<IUserDataService>();
           // mock.SetupAllProperties();

            mock.Setup(s => s.GetUserByEmail(It.IsAny<string>()))
                .Returns((string email) => _users.Single(u => u.Email == email));

            mock.Setup(s => s.UserExistsByEmail(It.IsAny<string>()))
                .Returns((string email) => _users.Any(u => u.Email == email));

            mock.Setup(s => s.CreateSession(It.IsAny<IUser>()))
                .Returns((IUser user) => new SessionBuilder()
                                             .WithUser(user)
                                             .WithCreatedOn(DateTime.UtcNow)
                                             .Build());

            mock.Setup(s => s.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns((
                    string name,
                    string email,
                    string passwordHash)
                         => new UserBuilder()
                                .BuildUser(email, passwordHash, name));

            return mock.Object;
        }
    }
}