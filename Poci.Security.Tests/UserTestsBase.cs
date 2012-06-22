using Poci.Common.Security;
using Poci.Common.Validation;
using Poci.Security.Tests.Builders;
using Xunit;

namespace Poci.Security.Tests
{
    public abstract class UserTestsBase
    {
        protected readonly IHashService HashService = new MD5HashService();

        [Fact]
        public virtual void can_log_on()
        {
            var securityService = GetSecurityService();

            var session = securityService.LogOn(
                new UserBuilder()
                    .BuildLogOn(UserBuilder.UserEmail, UserBuilder.CorrectPassword)
                );

            Assert.NotNull(session);
            Assert.NotNull(session.User);
            Assert.Equal(UserBuilder.UserEmail, session.User.Email);
            Assert.Equal(HashService.Hash64(UserBuilder.CorrectPassword), session.User.PasswordHash);
        }

        [Fact]
        public virtual void cannot_log_on_with_incorrect_credentials()
        {
            var securityService = GetSecurityService();

            var session = securityService.LogOn(
                new UserBuilder()
                    .BuildLogOn(UserBuilder.UserEmail, "someoldrubbish")
                );

            Assert.Null(session);
        }

        [Fact]
        public virtual void cannot_log_on_inactive_user_with_correct_credentials()
        {
            var securityService = GetSecurityService();

            var session = securityService.LogOn(
                new UserBuilder()
                    .BuildLogOn(UserBuilder.InactiveUserEmail, UserBuilder.CorrectPassword)
                );

            Assert.Null(session);
        }

        [Fact]
        public virtual void can_register()
        {
            var securityService = GetSecurityService();

            var session = securityService
                .Register(
                    new UserBuilder()
                        .BuildRegister(
                            UserBuilder.UserName, UserBuilder.RegisterUserEmail,
                            UserBuilder.CorrectPassword, UserBuilder.CorrectPassword)
                );

            Assert.NotNull(session);
            Assert.NotNull(session.User);
            Assert.Equal(UserBuilder.UserName, session.User.Name);
            Assert.Equal(UserBuilder.RegisterUserEmail, session.User.Email);
            Assert.Equal(HashService.Hash64(UserBuilder.CorrectPassword), session.User.PasswordHash);
        }

        [Fact]
        public virtual void can_not_register_with_short_password()
        {
            var securityService = GetSecurityService();

            Assert.Throws<ValidationResultsException>(
                () => securityService
                          .Register(
                              new UserBuilder()
                                  .BuildRegister(
                                      UserBuilder.UserName, UserBuilder.RegisterUserEmail,
                                      "a", "a")
                          )
                );
        }

        [Fact]
        public virtual void session_is_valid_from_log_on()
        {
            var securityService = GetSecurityService();

            var session = securityService.LogOn(
                new UserBuilder()
                    .BuildLogOn(UserBuilder.UserEmail, UserBuilder.CorrectPassword)
                );

            Assert.True(
                securityService.SessionIsValid(session), "session should be valid");
        }

        [Fact]
        public virtual void session_is_valid_from_register()
        {
            var securityService = GetSecurityService();

            var session = securityService.Register(
                new UserBuilder()
                    .BuildRegister(
                        UserBuilder.UserName,
                        UserBuilder.RegisterUserEmail,
                        UserBuilder.CorrectPassword,
                        UserBuilder.CorrectPassword)
                );

            Assert.True(
                securityService.SessionIsValid(session), "session should be valid");
        }

        [Fact]
        public virtual void session_is_not_valid_when_made_up()
        {
            var securityService = GetSecurityService();

            var session = new SessionBuilder()
                .WithUser(
                    new UserBuilder()
                        .Build(UserBuilder.UserEmail)
                )
                .Build();

            Assert.False(
                securityService.SessionIsValid(session), "session should not be valid");
        }

        protected abstract ISecurityService GetSecurityService();
    }
}