using System;
using System.Collections.Generic;

namespace Poci.Common.Services
{
    public interface IServiceContainer
    {
        T GetService<T>();
        object GetService(Type type);

        IEnumerable<T> GetServices<T>();
        IEnumerable<object> GetServices(Type type);

        IServiceRegistration<T> Register<T>();
    }

    public interface IServiceRegistration<T>
    {
        IServiceRegistration<TAnd> Register<TAnd>();
        IServiceContainer As<TImplementation>(Func<TImplementation> getService, ServiceLifeTime lifeTime)
            where TImplementation : T;
    }
}