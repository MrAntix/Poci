using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Poci.Security.Data;
using Poci.Security.DataServices;
using Testing;

namespace Poci.Security.Tests.Builders
{
    public class SessionDataServiceBuilder
    {
        public readonly IList<ISession> Sessions = new List<ISession>();

        readonly Builder<ISession> _sessionBuilder
            = new Builder<ISession>(Mock.Of<ISession>);

        public SessionDataServiceBuilder WithSession(
            ISession session)
        {
            Sessions.Add(session);

            return this;
        }

        public ISessionDataService Build()
        {
            var mock = new Mock<ISessionDataService>();
            mock.SetupAllProperties();

            mock.Setup(s => s.SessionExists(It.IsAny<Guid>(), It.IsAny<IUser>(), It.IsAny<bool>()))
                .Returns(
                    (Guid identifier,
                     IUser user,
                     bool includeExpired)
                    => Sessions.Any(
                        x => x.Identifier == identifier
                             && x.User.Active
                             && x.User.Email == user.Email
                             && (includeExpired || x.ExpiresOn > DateTime.UtcNow
                                ))
                );

            mock.Setup(s => s.CreateSession(It.IsAny<IUser>()))
                .Returns((IUser user) => _sessionBuilder
                                             .Build(s => { s.User = user; }));

            mock.Setup(s => s.InsertSession(It.IsAny<ISession>()))
                .Callback((ISession session) => Sessions.Add(session));

            return mock.Object;
        }
    }
}