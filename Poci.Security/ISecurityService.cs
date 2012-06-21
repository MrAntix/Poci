using System;
using Poci.Security.Data;

namespace Poci.Security
{
    public interface ISecurityService :
        IDisposable
    {
        ISession LogOn(IUserLogOn user);
        ISession Register(IUserRegister user);
        bool SessionIsValid(ISession session);
        void AssertSessionIsValid(ISession session);
    }
}