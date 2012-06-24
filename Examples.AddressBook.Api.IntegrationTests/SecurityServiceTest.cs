using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using Examples.AddressBook.Api.Models;
using Xunit;

namespace Examples.AddressBook.Api.IntegrationTests
{
    public class SecurityServiceTest :
        WebApiTestBase
    {
        [Fact]
        public void CanRegisterWithCorrectModel()
        {

            var client = new HttpClient(Server);
            var request = CreateRequest(
                "api/Register/", "application/json", HttpMethod.Post,
                new UserRegister
                    {
                        Name = "Test",
                        Email = "test@example.com",
                        Password = "test",
                        PasswordConfirm = "test"
                    });

            using (var response = client.SendAsync(request).Result)
            {
                Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
                Assert.NotNull(response.Content);
                var guid = response.Content.ReadAsAsync<Guid>().Result;

                Assert.NotEqual(Guid.Empty, guid);

            }

            request.Dispose();

        }

    }
}