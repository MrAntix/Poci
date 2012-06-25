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

        public EFSessionDataService(EFDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region ISessionDataService Members

        ISession ISessionDataService.CreateSession(IUser user)
        {
            return new EFSession
                       {
                           User = (EFUser) user
                       };
        }

        void ISessionDataService.InsertSession(ISession session)
        {
            _dataContext.Sessions.Add((EFSession) session);
        }

        void ISessionDataService.UpdateSession(ISession session)
        {
        }

        bool ISessionDataService.SessionExists(Guid identifier, IUser user, bool includeExpired)
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