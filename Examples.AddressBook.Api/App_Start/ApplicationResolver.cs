using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Examples.AddressBook.Api.Controllers;
using Poci.Common.Security;
using Poci.Common.Services;
using Poci.Security;
using Poci.Security.DataServices;
using Poci.Security.Validation;

namespace Examples.AddressBook.Api.App_Start
{
    public class ApplicationResolver :
        IServiceContainer,
        IDependencyResolver
    {
        #region IDependencyResolver Members

        public virtual object GetService(Type serviceType)
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

            if (serviceType == typeof (IHashService))
                return new MD5HashService();

            if (serviceType == typeof (IUserRegistrationValidator))
                return new UserRegistrationValidator();

            return null;
        }

        public virtual IEnumerable<object> GetServices(Type serviceType)
        {
            return new object[] {};
        }

        public IServiceRegistration<TI> Register<TI>()
        {
            throw new NotImplementedException();
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
            }

            // unmanaged resources here.

            _disposed = true;
        }

        #endregion

        #region IServiceContainer Members

        public T GetService<T>()
        {
            return (T) GetService(typeof (T));
        }

        public IEnumerable<T> GetServices<T>()
        {
            return GetServices(typeof (T)).Cast<T>();
        }

      

        #endregion

        IDictionary<Type, object> _registrations;
    }
}