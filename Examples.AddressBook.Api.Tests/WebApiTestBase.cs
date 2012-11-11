using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Poci.Common.DataServices;
using Poci.Common.Security;
using Poci.Common.Services;

namespace Examples.AddressBook.Api.Tests
{
    public abstract class WebApiTestBase<TDataContext> :
        IDisposable
        where TDataContext : IDataContext
    {
        protected readonly TDataContext DataContext;
        protected readonly IHashService HashService;
        protected readonly HttpServer Server;
        protected string RootUrl = "http://test.example/";

        protected WebApiTestBase(
            IServiceContainer dependencyResolver)
        {
            DataContext = dependencyResolver.GetService<TDataContext>();
            HashService = dependencyResolver.GetService<IHashService>();

            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "Default",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
                );

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            // config.DependencyResolver = dependencyResolver;

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

        HttpRequestMessage CreateGet(
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