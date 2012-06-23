using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Examples.AddressBook.Api.Controllers;
using Examples.AddressBook.InMemory.DataService;
using Poci.Common.Security;
using Poci.Security;
using Poci.Security.DataServices;
using Poci.Security.Validation;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace Examples.AddressBook.Api
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.DependencyResolver
                = new ApplicationResolver();
        }
    }

    public class ApplicationResolver :
        IDependencyResolver
    {
        readonly InMemoryDataContext _dataContext
            = new InMemoryDataContext();

        #region IDependencyResolver Members

        public void Dispose()
        {
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(RegisterController))
            {
                return new RegisterController(
                    GetService<ISecurityService>()
                    );
            }

            if (serviceType == typeof (ISecurityService))
            {
                return new SecurityService(
                    GetService<IUserDataService>(),
                    GetService<ISessionDataService>(),
                    GetService<IHashService>(),
                    GetService<IUserRegistrationValidator>()
                    );
            }

            if (serviceType == typeof (IUserDataService))
            {
                return new InMemoryUserDataService(_dataContext);
            }

            if (serviceType == typeof (ISessionDataService))
            {
                return new InMemorySessionDataService(_dataContext);
            }

            if (serviceType == typeof (IHashService))
            {
                return new MD5HashService();
            }

            if (serviceType == typeof (IUserRegistrationValidator))
            {
                return new UserRegistrationValidator();
            }

            return null;
        }


        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new object[] {};
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        #endregion

        public T GetService<T>()
        {
            return (T) GetService(typeof (T));
        }
    }
}