using System;
using Poci.Security.Data;

namespace Poci.Security
{
    public interface IUserService: IDisposable
    {
        ISession LogOn(IUserLogOn user);
        ISession Register(IUserRegister user);
    }
}
