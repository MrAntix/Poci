using System.Collections.Generic;
using System.Linq;
using Moq;
using Poci.Security.Data;
using Poci.Security.DataServices;

namespace Poci.Security.Tests.Builders
{
    public class UserDataServiceBuilder
    {
        public readonly IList<IUser> Users = new List<IUser>();

        public UserDataServiceBuilder WithUser(
            IUser user)
        {
            Users.Add(user);

            return this;
        }

        public IUserDataService Build()
        {
            var mock = new Mock<IUserDataService>();
            mock.SetupAllProperties();

            mock.Setup(s => s.GetUser(It.IsAny<string>()))
                .Returns((string email) => Users.Single(u => u.Email == email));

            mock.Setup(s => s.UserExists(It.IsAny<string>()))
                .Returns((string email) => Users.Any(u => u.Email == email));

            mock.Setup(s => s.InsertUser(It.IsAny<IUser>()))
                .Callback((IUser user) => Users.Add(user));

            mock.Setup(s => s.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns((
                    string name,
                    string email,
                    string passwordHash)
                         => new UserBuilder()
                                .Build(email, passwordHash, name));

            return mock.Object;
        }
    }
}