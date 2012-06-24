using System;
using System.Net;
using System.Net.Http;
using Examples.AddressBook.Api.Models;
using Xunit;

namespace Examples.AddressBook.Api.IntegrationTests
{
    public class SecurityServiceTest :
        WebApiTestBase
    {
        [Fact]
        public async void CanRegisterWithCorrectModel()
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

            using (var response = await client.SendAsync(request))
            {
                Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
                Assert.NotNull(response.Content);
                var guid = await response.Content.ReadAsAsync<Guid>();

                Assert.NotEqual(Guid.Empty, guid);
            }

            request.Dispose();
        }
    }
}