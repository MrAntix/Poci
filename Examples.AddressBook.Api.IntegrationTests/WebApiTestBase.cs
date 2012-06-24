using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Examples.AddressBook.Api.IntegrationTests
{
    public abstract class WebApiTestBase :
        IDisposable
    {
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
            config.DependencyResolver = new ApplicationResolver();

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

        protected HttpRequestMessage CreateRequest(
            string url, string contentType,
            HttpMethod method)
        {
            var request = new HttpRequestMessage
                              {
                                  RequestUri = new Uri(RootUrl + url)
                              };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            request.Method = method;

            return request;
        }

        protected HttpRequestMessage CreateRequest<T>(
            string url, string contentType,
            HttpMethod method,
            T content,
            MediaTypeFormatter formatter = null) where T : class
        {
            var request = CreateRequest(url, contentType, method);
            request.Content = new ObjectContent<T>(
                content,
                formatter ?? new JsonMediaTypeFormatter());

            return request;
        }
    }
}