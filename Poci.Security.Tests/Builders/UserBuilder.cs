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

        public IUser BuildUser(
            string email,
            string passwordHash = "passwordHash",
            string name = "A User",
            bool active = true)
        {
            var mock = new Mock<IUser>();
            mock.SetupAllProperties();

            mock
                .Setup(o => o.Name)
                .Returns(name);
            mock
                .Setup(o => o.Email)
                .Returns(email);
            mock
                .Setup(o => o.PasswordHash)
                .Returns(passwordHash);
            mock
                .Setup(o => o.Active)
                .Returns(active);

            return mock.Object;
        }

        public IUserLogOn BuildLogOn(
            string email,
            string password)
        {
            var mock = new Mock<IUserLogOn>();
            mock.SetupAllProperties();

            mock
                .Setup(o => o.Email)
                .Returns(email);
            mock
                .Setup(o => o.Password)
                .Returns(password);

            return mock.Object;
        }

        public IUserRegister BuildRegister(
            string name,
            string email,
            string password, string passwordConfirm)
        {
            var mock = new Mock<IUserRegister>();
            mock.SetupAllProperties();

            mock
                .Setup(o => o.Name)
                .Returns(name);
            mock
                .Setup(o => o.Email)
                .Returns(email);
            mock
                .Setup(o => o.Password)
                .Returns(password);
            mock
                .Setup(o => o.PasswordConfirm)
                .Returns(passwordConfirm);

            return mock.Object;
        }
    }
}