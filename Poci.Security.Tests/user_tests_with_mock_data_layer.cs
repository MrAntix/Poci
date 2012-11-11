using Moq;
using Poci.Security.Data;
using Poci.Security.Tests.Builders;
using Poci.Security.Validation;
using Testing;

namespace Poci.Security.Tests
{
    public class user_tests_with_mock_data_layer : UserTestsBase
    {
        public user_tests_with_mock_data_layer()
            : base(
                GetLogOnBuilder(),
                GetUserRegistationBuilder(),
                GetSessionBuilder()
                )
        {
        }

        static Builder<ISession> GetSessionBuilder()
        {
            var userBuilder = new Builder<IUser>(Mock.Of<IUser>)
                .With(u => { u.Email = SecurityTestData.User.Email; });

            return new Builder<ISession>(Mock.Of<ISession>)
                .With(s => { s.User = userBuilder.Build(); });
        }

        static Builder<IUserLogOn> GetLogOnBuilder()
        {
            return new Builder<IUserLogOn>(Mock.Of<IUserLogOn>)
                .With(u =>
                          {
                              u.Email = SecurityTestData.User.Email;
                              u.Password = SecurityTestData.User.CorrectPassword;
                          });
        }

        static Builder<IUserRegister> GetUserRegistationBuilder()
        {
            return new Builder<IUserRegister>(Mock.Of<IUserRegister>)
                .With(u =>
                          {
                              u.Name = SecurityTestData.User.Name;
                              u.Email = SecurityTestData.User.RegisterEmail;
                              u.Password = SecurityTestData.User.CorrectPassword;
                              u.PasswordConfirm = SecurityTestData.User.CorrectPassword;
                          });
        }

        protected override ISecurityService GetSecurityService()
        {
            var userBuilder = new Builder<IUser>(Mock.Of<IUser>)
                .With(u =>
                          {
                              u.Email = SecurityTestData.User.Email;
                              u.PasswordHash = HashService.Hash64(SecurityTestData.User.CorrectPassword);
                              u.Active = true;
                          });

            var user = userBuilder.Build();
            var inactiveUser = userBuilder
                .Build(u =>
                          {
                              u.Email = SecurityTestData.User.InactiveEmail;
                              u.Active = false;
                          });

            return new SecurityService(
                new UserDataServiceBuilder()
                    .WithUser(user)
                    .WithUser(inactiveUser)
                    .Build(),
                new SessionDataServiceBuilder()
                    .Build(),
                HashService,
                new UserRegistrationValidator()
                );
        }
    }
}