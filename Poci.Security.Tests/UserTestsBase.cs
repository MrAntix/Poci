using System;
using System.Diagnostics.CodeAnalysis;
using Poci.Common.Security;
using Poci.Common.Validation;
using Poci.Security.Data;
using Poci.Testing;
using Xunit;

namespace Poci.Security.Tests
{
    public abstract class UserTestsBase :
        IDisposable
    {
        protected readonly IHashService HashService = new MD5HashService();

        readonly Builder<IUserLogOn> _logOnBuilder;
        readonly Builder<ISession> _sessionBuilder;
        readonly Builder<IUserRegister> _userRegistationBuilder;

        protected UserTestsBase(
            Builder<IUserLogOn> logOnBuilder,
            Builder<IUserRegister> userRegistationBuilder, 
            Builder<ISession> sessionBuilder)
        {
            _logOnBuilder = logOnBuilder;
            _userRegistationBuilder = userRegistationBuilder;
            _sessionBuilder = sessionBuilder;
        }

        #region IDisposable Members

        /// <summary>
        ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
        public void Dispose()
        {
            HashService.Dispose();
        }

        #endregion

        [Fact]
        public virtual void CanLogOn()
        {
            var securityService = GetSecurityService();

            var session = securityService.LogOn(_logOnBuilder.Build());

            Assert.NotNull(session);
            Assert.NotNull(session.User);
            Assert.Equal(TestData.User.Email, session.User.Email);
            Assert.Equal(HashService.Hash64(TestData.User.CorrectPassword), session.User.PasswordHash);
        }

        [Fact]
        public virtual void CannotLogOnWithIncorrectCredentials()
        {
            var securityService = GetSecurityService();

            var session = securityService.LogOn(
                _logOnBuilder.Build(
                    u => { u.Password = "someoldrubbish"; }
                    ));

            Assert.Null(session);
        }

        [Fact]
        public virtual void CannotLogOnInactiveUserWithCorrectCredentials()
        {
            var securityService = GetSecurityService();

            var session = securityService.LogOn(
                _logOnBuilder.Build(
                    u => { u.Email = TestData.User.InactiveEmail; }
                    ));

            Assert.Null(session);
        }

        [Fact]
        public virtual void CanRegister()
        {
            var securityService = GetSecurityService();

            var session = securityService
                .Register(_userRegistationBuilder.Build());

            Assert.NotNull(session);
            Assert.NotNull(session.User);
            Assert.Equal(TestData.User.Name, session.User.Name);
            Assert.Equal(TestData.User.RegisterEmail, session.User.Email);
            Assert.Equal(HashService.Hash64(TestData.User.CorrectPassword), session.User.PasswordHash);
        }

        [Fact]
        public virtual void CanNotRegisterWithShortPassword()
        {
            var securityService = GetSecurityService();

            Assert.Throws<ValidationResultsException>(
                () => securityService.Register(
                    _userRegistationBuilder.Build(
                        u =>
                            {
                                u.Password = "a";
                                u.PasswordConfirm = "a";
                            }
                        ))
                );
        }

        [Fact]
        public virtual void CanNotRegisterWithPasswordConfirmMismatch()
        {
            var securityService = GetSecurityService();

            Assert.Throws<ValidationResultsException>(
                () => securityService.Register(
                    _userRegistationBuilder.Build(
                        u =>
                            {
                                u.Password = "abcde";
                                u.PasswordConfirm = "abcdef";
                            }
                        ))
                );
        }

        [Fact]
        public virtual void CanNotRegisterWithExistingEmail()
        {
            var securityService = GetSecurityService();

            Assert.Null(
                securityService.Register(
                    _userRegistationBuilder.Build(
                        u => { u.Email = TestData.User.Email; }
                        )
                    )
                );
        }

        [Fact]
        public virtual void CanNotRegisterWithNullPassword()
        {
            var securityService = GetSecurityService();

            Assert.Throws<ValidationResultsException>(
                () => securityService.Register(
                    _userRegistationBuilder.Build(
                        u =>
                            {
                                u.Password = null;
                                u.PasswordConfirm = null;
                            }))
                );
        }

        [Fact]
        public virtual void SessionIsValidFromLogOn()
        {
            var securityService = GetSecurityService();

            var session = securityService
                .LogOn(_logOnBuilder.Build());

            Assert.True(
                securityService.SessionIsValid(session), "session should be valid");
        }

        [Fact]
        public virtual void SessionIsValidFromRegister()
        {
            var securityService = GetSecurityService();

            var session = securityService
                .Register(_userRegistationBuilder.Build());

            Assert.True(
                securityService.SessionIsValid(session), "session should be valid");
        }

        [Fact]
        public virtual void SessionIsNotValidWhenMadeUp()
        {
            var securityService = GetSecurityService();
            var session = _sessionBuilder.Build();

            Assert.False(
                securityService.SessionIsValid(session), "session should not be valid");
        }

        protected abstract ISecurityService GetSecurityService();
    }
}