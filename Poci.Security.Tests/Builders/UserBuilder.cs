using Moq;
using Poci.Security.Data;

namespace Poci.Security.Tests.Builders
{
    public class UserBuilder
    {
        public const string UserName = "userName";
        public const string UserEmail = "user@example.com";
        public const string InactiveUserEmail = "inactive.user@example.com";
        public const string RegisterUserEmail = "register.user@example.com";
        public const string CorrectPassword = "correctPassword";

        public IUser Build(
            string email,
            string passwordHash = "passwordHash",
            string name = "A User",
            bool active = true)
        {
            var user = Mock.Of<IUser>();

            user.Name = name;
            user.Email = email;
            user.PasswordHash = passwordHash;
            user.Active = active;

            return user;
        }

        public IUserLogOn BuildLogOn(
            string email,
            string password)
        {
            var user = Mock.Of<IUserLogOn>();

            user.Email = email;
            user.Password = password;

            return user;
        }

        public IUserRegister BuildRegister(
            string name,
            string email,
            string password, string passwordConfirm)
        {
            var user = Mock.Of<IUserRegister>();

            user.Name = name;
            user.Email = email;
            user.Password = password;
            user.PasswordConfirm = passwordConfirm;

            return user;
        }
    }
}