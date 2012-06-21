using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Poci.Security.Data;
using Poci.Security.DataServices;

namespace Poci.Security.Tests.Builders
{
    public class SessionDataServiceBuilder
    {
        public readonly IList<ISession> Sessions = new List<ISession>();

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
                .Returns((IUser user) => new SessionBuilder()
                                             .WithUser(user)
                                             .Build());

            mock.Setup(s => s.InsertSession(It.IsAny<ISession>()))
                .Callback((ISession session) => Sessions.Add(session));

            return mock.Object;
        }
    }
}