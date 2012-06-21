using Poci.Security.Tests.Builders;
using Poci.Security.Validation;

namespace Poci.Security.Tests
{
    public class user_tests_with_mock_data_layer : UserTestsBase
    {
        protected override ISecurityService GetSecurityService()
        {
            var userBuilder = new UserBuilder();
            var user = userBuilder
                .Build(
                    UserBuilder.UserEmail,
                    HashService.Hash64(UserBuilder.CorrectPassword));
            var inactiveUser = userBuilder
                .Build(
                    UserBuilder.InactiveUserEmail,
                    HashService.Hash64(UserBuilder.CorrectPassword), active: false);

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