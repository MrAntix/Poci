using System;
using Poci.Security.Data;

namespace Poci.Security.DataServices
{
    public interface ISessionDataService
    {
        ISession CreateSession(
            IUser user);

        void InsertSession(
            ISession session);

        void UpdateSession(
            ISession session);

        bool SessionExists(
            Guid identifier, IUser user, bool includeExpired);
    }
}