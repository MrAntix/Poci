using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Examples.AddressBook.Api.Controllers;
using Examples.AddressBook.EF.DataService;
using Poci.Common.Security;
using Poci.Security;
using Poci.Security.DataServices;
using Poci.Security.Validation;

namespace Examples.AddressBook.Api.App_Start
{
    public class ApplicationResolver :
        IDependencyResolver
    {
        readonly EFDataContext _dataContext
            = new EFDataContext();

        #region IDependencyResolver Members

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof (RegisterController))
                return new RegisterController(
                    GetService<ISecurityService>()
                    );

            if (serviceType == typeof (LogOnController))
                return new LogOnController(
                    GetService<ISecurityService>()
                    );

            if (serviceType == typeof (ISecurityService))
                return new SecurityService(
                    GetService<IUserDataService>(),
                    GetService<ISessionDataService>(),
                    GetService<IHashService>(),
                    GetService<IUserRegistrationValidator>()
                    );

            if (serviceType == typeof (IUserDataService))
                return new EFUserDataService(_dataContext);

            if (serviceType == typeof (ISessionDataService))
                return new EFSessionDataService(_dataContext);

            if (serviceType == typeof (IHashService))
                return new MD5HashService();

            if (serviceType == typeof (IUserRegistrationValidator))
                return new UserRegistrationValidator();

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

        #region IDisposable

        bool _disposed;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // Dispose managed resources.
                _dataContext.Dispose();
            }

            // unmanaged resources here.

            _disposed = true;
        }

        #endregion

        public T GetService<T>()
        {
            return (T) GetService(typeof (T));
        }
    }
}