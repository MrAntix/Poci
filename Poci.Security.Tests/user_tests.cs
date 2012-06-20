using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poci.Common.Security;
using Poci.Security.Tests.Builders;
using Poci.Security.Validation;

namespace Poci.Security.Tests
{
    [TestClass]
    public class user_tests
    {
        const string UserName = "userName";
        const string UserEmail = "user@example.com";
        const string InactiveUserEmail = "inactive.user@example.com";
        const string RegisterUserEmail = "register.user@example.com";
        const string CorrectPassword = "correctPassword";
        readonly IHashService _hashService = new MD5HashService();

        [TestMethod]
        public void can_log_on()
        {
            using (var userService = GetUserService())
            {
                var session = userService.LogOn(
                    new UserBuilder()
                        .BuildLogOn(UserEmail, CorrectPassword)
                    );

                Assert.IsNotNull(session, "expected a session");
                Assert.IsNotNull(session.User, "expected a session.User");
                Assert.AreEqual(UserEmail, session.User.Email, "expected matching session.User.Email");
                Assert.AreEqual(_hashService.Hash64(CorrectPassword), session.User.PasswordHash,
                                "expected matching session.User.PasswordHash");
            }
        }

        [TestMethod]
        public void cannot_log_on_with_incorrect_credentials()
        {
            using (var userService = GetUserService())
            {
                var session = userService.LogOn(
                    new UserBuilder()
                        .BuildLogOn(UserEmail,"someoldrubbish")
                        );

                Assert.IsNull(session, "expected no session");
            }
        }

        [TestMethod]
        public void cannot_log_on_inactive_user_with_correct_credentials()
        {
            using (var userService = GetUserService())
            {
                var session = userService.LogOn(
                    new UserBuilder()
                        .BuildLogOn(InactiveUserEmail, CorrectPassword)
                    );

                Assert.IsNull(session, "expected no session");
            }
        }

        [TestMethod]
        public void can_register()
        {
            using (var userService = GetUserService())
            {
                var session = userService
                    .Register(
                        new UserBuilder()
                            .BuildRegister(
                                UserName, RegisterUserEmail,
                                CorrectPassword, CorrectPassword)
                    );

                Assert.IsNotNull(session, "expected a session");
                Assert.IsNotNull(session.User, "expected a session.User");
                Assert.AreEqual(UserName, session.User.Name, "expected matching session.User.Name");
                Assert.AreEqual(RegisterUserEmail, session.User.Email, "expected matching session.User.Email");
                Assert.AreEqual(_hashService.Hash64(CorrectPassword), session.User.PasswordHash,
                                "expected matching session.User.PasswordHash");
            }
        }

        IUserService GetUserService()
        {
            var userBuilder = new UserBuilder();

            return new UserService(
                new UserDataServiceBuilder(_hashService)
                    .WithUser(userBuilder.BuildUser(UserEmail, _hashService.Hash64(CorrectPassword)))
                    .WithUser(userBuilder.BuildUser(InactiveUserEmail, _hashService.Hash64(CorrectPassword), active: false))
                    .Build(),
                _hashService,
                new UserRegistrationValidator()
                );
        }
    }
}