using System;
using System.Linq;
using Examples.AddressBook.EF.DataService;
using Xunit;

namespace Examples.AddressBook.EF.Tests
{
    public class DataContextTest
    {
        [Fact]
        public void CanConnect()
        {
            var dc = new EFDataContext();

            if (dc.Users.Any())
            {
                var user = dc.Users.First();
                Assert.NotNull(user.Name);
            }

            throw new Exception("no users in db");
        }
    }
}
