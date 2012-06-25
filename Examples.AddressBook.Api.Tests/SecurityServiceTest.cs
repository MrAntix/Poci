using System;
using System.Net;
using System.Net.Http;
using Examples.AddressBook.Api.Models;
using Examples.AddressBook.InMemory.Data;
using Xunit;

namespace Examples.AddressBook.Api.Tests
{
    public class SecurityServiceTest :
        WebApiTestBase
    {
        const string UserName = "Test User";
        const string UserEmail = "test@example.com";
        const string UserPassword = "t3st";

        [Fact]
        public void CanRegisterWithCorrectModel()
        {
            var client = new HttpClient(Server);
            var request = CreatePost(
                "api/Register/", 
                new UserRegister
                    {
                        Name = UserName,
                        Email = UserEmail,
                        Password = UserPassword,
                        PasswordConfirm = UserPassword
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

        [Fact]
        public void CanLogOnWithCorrectModel()
        {
            DataContext.Users.Add(
                new InMemoryUser
                    {
                        Active = true,
                        Name = UserName,
                        Email = UserEmail,
                        PasswordHash = HashService.Hash64(UserPassword)
                    });

            var client = new HttpClient(Server);
            var request = CreatePost(
                "api/LogOn/", 
                new UserLogOn
                {
                    Email = UserEmail,
                    Password = UserPassword
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