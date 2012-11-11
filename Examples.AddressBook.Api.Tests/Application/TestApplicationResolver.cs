using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Examples.AddressBook.Api.Controllers;
using Examples.AddressBook.InMemory.DataService;
using Poci.Common.Security;
using Poci.Common.Services;
using Poci.Security;
using Poci.Security.DataServices;
using Poci.Security.Validation;

namespace Examples.AddressBook.Api.Tests.Application
{
    public class TestApplicationResolver :
        IServiceContainer, IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            if (_registrationKeys.ContainsKey(serviceType))
                serviceType = _registrationKeys[serviceType];

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
                return new InMemoryUserDataService(GetService<InMemoryDataContext>());

            if (serviceType == typeof (ISessionDataService))
                return new InMemorySessionDataService(GetService<InMemoryDataContext>());

            if (serviceType == typeof (IUserRegistrationValidator))
                return new UserRegistrationValidator();

            if (!_registrations.ContainsKey(serviceType))
                throw new Exception(string.Format("{0} not registered", serviceType.Name));

            return _registrations[serviceType]();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new object[] {};
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public T GetService<T>()
        {
            return (T) GetService(typeof (T));
        }

        public IEnumerable<T> GetServices<T>()
        {
            return GetServices(typeof (T)).Cast<T>();
        }

        IDictionary<Type, Type> _registrationKeys = new Dictionary<Type, Type>();
        IDictionary<Type, Func<object>> _registrations = new Dictionary<Type, Func<object>>();
        Cache _sessionCache = new Cache();
        Cache _singletonCache = new Cache();

        public IServiceRegistration<T> Register<T>()
        {
            return new ServiceRegistration<T>(this);
        }

        public class ServiceRegistration<T> :
            IServiceRegistration<T>
        {
            TestApplicationResolver _resolver;
            IList<Type> _types;

            public ServiceRegistration(
                TestApplicationResolver resolver,
                IList<Type> types = null)
            {
                _resolver = resolver;

                _types = types ?? new List<Type>();
                _types.Add(typeof (T));
            }

            public IServiceRegistration<TAnd> Register<TAnd>()
            {
                return new ServiceRegistration<TAnd>(_resolver, _types);
            }

            public IServiceContainer As<TImplementation>(
                Func<TImplementation> getService, ServiceLifeTime lifeTime) where TImplementation : T
            {
                var keyType = _types.First();
                if (_resolver._registrations.ContainsKey(keyType))
                    throw new ArgumentException(
                        string.Format("A service is already registered for '{0}'", keyType.FullName),
                        "getService");

                foreach(var type in _types.Skip(1))
                    _resolver._registrationKeys.Add(type, keyType);

                var fromCache =
                    (Func<IDictionary<Type, object>, Type, Func<TImplementation>, object>)
                    ((c, t, n) =>
                         {
                             if (c.ContainsKey(t)) return c[t];
                             var o = n();
                             c.Add(t, o);

                             return o;
                         });

                switch (lifeTime)
                {
                    case ServiceLifeTime.Singleton:
                        _resolver._registrations.Add(keyType, () => fromCache(_resolver._singletonCache, keyType, getService));

                        break;
                    case ServiceLifeTime.Session:
                        _resolver._registrations.Add(keyType, () => fromCache(_resolver._sessionCache, keyType, getService));

                        break;
                    case ServiceLifeTime.Transient:
                        _resolver._registrations.Add(keyType, () => getService());

                        break;
                }

                return _resolver;
            }
        }

        class Cache :
            Dictionary<Type, object>,
            IDisposable
        {
            public void Dispose()
            {
                foreach (var disposable in Values.OfType<IDisposable>())
                    disposable.Dispose();
            }
        }

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
                _singletonCache.Dispose();
                _sessionCache.Dispose();
            }

            // unmanaged resources here.

            _disposed = true;
        }

        #endregion
    }
}