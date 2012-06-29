using Examples.AddressBook.EF.DataService;
using Poci.Security.DataServices;
using Xunit;

namespace Examples.AddressBook.EF.Tests
{
    public class DataContextTest
    {
        [Fact]
        public void CanConnect()
        {
            using (var uow = new EFDataContext())
            {
                var userService = GetUserDataService(uow);
                var user = userService.TryGetUser(
                    "test@example.com"
                    );

                Assert.Null(user);
            }
        }

        IUserDataService GetUserDataService(
            EFDataContext uow)
        {
            return new EFUserDataService(uow);
        }
    }
}