using Moq;
using Poci.Security.Data;
using Poci.Security.Tests.Builders;
using Poci.Security.Validation;
using Poci.Testing;

namespace Poci.Security.Tests
{
    public class UserTestsWithMockDataLayer : UserTestsBase
    {
        public UserTestsWithMockDataLayer()
            : base(
                GetLogOnBuilder(),
                GetUserRegistationBuilder(),
                GetSessionBuilder()
                )
        {
        }

        static Builder<ISession> GetSessionBuilder()
        {
            var userBuilder = new Builder<IUser>()
                .CreateWith(Mock.Of<IUser>)
                .AssignWith(u => { u.Email = TestData.User.Email; });

            return new Builder<ISession>()
                .CreateWith(Mock.Of<ISession>)
                .AssignWith(s => { s.User = userBuilder.Build(); });
        }

        static Builder<IUserLogOn> GetLogOnBuilder()
        {
            return new Builder<IUserLogOn>()
                .CreateWith(Mock.Of<IUserLogOn>)
                .AssignWith(u =>
                                {
                                    u.Email = TestData.User.Email;
                                    u.Password = TestData.User.CorrectPassword;
                                });
        }

        static Builder<IUserRegister> GetUserRegistationBuilder()
        {
            return new Builder<IUserRegister>()
                .CreateWith(Mock.Of<IUserRegister>)
                .AssignWith(u =>
                                {
                                    u.Name = TestData.User.Name;
                                    u.Email = TestData.User.RegisterEmail;
                                    u.Password = TestData.User.CorrectPassword;
                                    u.PasswordConfirm = TestData.User.CorrectPassword;
                                });
        }

        protected override ISecurityService GetSecurityService()
        {
            var userBuilder = new Builder<IUser>()
                .CreateWith(Mock.Of<IUser>)
                .AssignWith(u =>
                                {
                                    u.Email = TestData.User.Email;
                                    u.PasswordHash = HashService.Hash64(TestData.User.CorrectPassword);
                                    u.Active = true;
                                });

            var user = userBuilder.Build();
            var inactiveUser = userBuilder.Build(u =>
                                                     {
                                                         u.Email = TestData.User.InactiveEmail;
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