using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poci.Common.Security;
using Poci.Security.Tests.Builders;
using Poci.Security.Validation;

namespace Poci.Security.Tests
{
    [TestClass]
    public class user_tests
    {
        readonly IHashService _hashService = new MD5HashService();

        [TestMethod]
        public void can_log_on()
        {
            using (var userService = GetUserService())
            {
                var session = userService.LogOn(
                    new UserBuilder()
                        .BuildLogOn(UserBuilder.UserEmail, UserBuilder.CorrectPassword)
                    );

                Assert.IsNotNull(session, "expected a session");
                Assert.IsNotNull(session.User, "expected a session.User");
                Assert.AreEqual(UserBuilder.UserEmail, session.User.Email, "expected matching session.User.Email");
                Assert.AreEqual(_hashService.Hash64(UserBuilder.CorrectPassword), session.User.PasswordHash,
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
                        .BuildLogOn(UserBuilder.UserEmail, "someoldrubbish")
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
                        .BuildLogOn(UserBuilder.InactiveUserEmail, UserBuilder.CorrectPassword)
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
                                UserBuilder.UserName, UserBuilder.RegisterUserEmail,
                                UserBuilder.CorrectPassword, UserBuilder.CorrectPassword)
                    );

                Assert.IsNotNull(session, "expected a session");
                Assert.IsNotNull(session.User, "expected a session.User");
                Assert.AreEqual(UserBuilder.UserName, session.User.Name, "expected matching session.User.Name");
                Assert.AreEqual(UserBuilder.RegisterUserEmail, session.User.Email,
                                "expected matching session.User.Email");
                Assert.AreEqual(_hashService.Hash64(UserBuilder.CorrectPassword), session.User.PasswordHash,
                                "expected matching session.User.PasswordHash");
            }
        }

        ISecurityService GetUserService()
        {
            var userBuilder = new UserBuilder();

            return new SecurityService(
                new UserDataServiceBuilder(_hashService)
                    .WithUser(userBuilder.BuildUser(UserBuilder.UserEmail,
                                                    _hashService.Hash64(UserBuilder.CorrectPassword)))
                    .WithUser(userBuilder.BuildUser(UserBuilder.InactiveUserEmail,
                                                    _hashService.Hash64(UserBuilder.CorrectPassword), active: false))
                    .Build(),
                _hashService,
                new UserRegistrationValidator()
                );
        }
    }
}