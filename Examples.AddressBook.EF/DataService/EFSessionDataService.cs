using System;
using System.Linq;
using Examples.AddressBook.EF.Data;
using Poci.Security.Data;
using Poci.Security.DataServices;

namespace Examples.AddressBook.EF.DataService
{
    public sealed class EFSessionDataService :
        ISessionDataService
    {
        readonly EFDataContext _dataContext;

        public EFSessionDataService(
            EFDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region ISessionDataService Members

        ISession ISessionDataService.
            CreateSession(IUser user)
        {
            var session = (ISession) new EFSession();
            session.User = user;

            return session;
        }

        void ISessionDataService.
            InsertSession(ISession session)
        {
        }

        void ISessionDataService.
            UpdateSession(ISession session)
        {
            //var data = GetSession(session.Identifier);
        }

        bool ISessionDataService.
            SessionExists(Guid identifier, IUser user, bool includeExpired)
        {
            var userEmail = user.Email;

            return _dataContext.
                Sessions.Any(s =>
                             s.Identifier == identifier
                             && s.User.Email == userEmail
                             && (includeExpired || s.ExpiresOn > DateTime.UtcNow)
                );
        }

        #endregion
    }
}