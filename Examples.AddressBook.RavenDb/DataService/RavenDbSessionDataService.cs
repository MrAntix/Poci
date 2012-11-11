using System;
using System.Linq;
using Examples.AddressBook.RavenDb.Data;
using Poci.Security.Data;
using Poci.Security.DataServices;

namespace Examples.AddressBook.RavenDb.DataService
{
    public class RavenDbSessionDataService :
        ISessionDataService
    {
        readonly RavenDbDataContext _dataContext;

        public RavenDbSessionDataService(RavenDbDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region ISessionDataService Members

        public ISession CreateSession(IUser user)
        {
            return new RavenDbSession
                       {
                           User = user
                       };
        }

        public void InsertSession(ISession session)
        {
            _dataContext.Sessions.Add(session);
        }

        public void UpdateSession(ISession session)
        {
        }

        public bool SessionExists(Guid identifier, IUser user, bool includeExpired)
        {
            return _dataContext
                .Sessions.Any(s =>
                              s.Identifier == identifier
                              && s.User == user
                              && (includeExpired || s.ExpiresOn > DateTime.UtcNow));
        }

        #endregion
    }
}