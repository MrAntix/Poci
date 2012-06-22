using System;
using System.Linq;
using Examples.AddressBook.InMemory.Data;
using Poci.Security.Data;
using Poci.Security.DataServices;

namespace Examples.AddressBook.InMemory.DataService
{
    public class InMemorySessionDataService:
        ISessionDataService
    {
        readonly InMemoryDataContext _dataContext;

        public InMemorySessionDataService(InMemoryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ISession CreateSession(IUser user)
        {
            return new InMemorySession
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
    }
}