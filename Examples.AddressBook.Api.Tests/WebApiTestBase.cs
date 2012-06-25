using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Examples.AddressBook.Api.Tests.Application;
using Examples.AddressBook.InMemory.DataService;
using Poci.Common.Security;

namespace Examples.AddressBook.Api.Tests
{
    public abstract class WebApiTestBase :
        IDisposable
    {
        protected readonly InMemoryDataContext DataContext;
        protected readonly IHashService HashService;
        protected readonly HttpServer Server;
        protected string RootUrl = "http://test.example/";

        protected WebApiTestBase()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "Default",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
                );

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            config.DependencyResolver = new ApplicationResolver(
                DataContext = new InMemoryDataContext(),
                HashService  = new MD5HashService());

            Server = new HttpServer(config);
        }

        #region IDisposable Members

        [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
        public void Dispose()
        {
            if (Server != null)
            {
                Server.Dispose();
            }
        }

        #endregion

        HttpRequestMessage CreateRequest(
            string url, 
            string contentType = null,
            HttpMethod method = null)
        {
            var request = new HttpRequestMessage
                              {
                                  RequestUri = new Uri(RootUrl + url)
                              };

            request.Headers.Accept.Add(
                new MediaTypeWithQualityHeaderValue(contentType ?? "application/json")
                );
            request.Method = method ?? HttpMethod.Get;

            return request;
        }

        private HttpRequestMessage CreateGet(
            string url,
            string contentType = null)
        {
            return CreateRequest(
                url,
                contentType,
                HttpMethod.Post);
        }

        protected HttpRequestMessage CreatePost<T>(
            string url,
            T content,
            string contentType = null,
            MediaTypeFormatter formatter = null) where T : class
        {
            var request = CreateRequest(
                url, 
                contentType, 
                HttpMethod.Post);

            request.Content = new ObjectContent<T>(
                content,
                formatter ?? new JsonMediaTypeFormatter());

            return request;
        }
    }
}