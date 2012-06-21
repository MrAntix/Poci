using System;
using Moq;
using Poci.Security.Data;

namespace Poci.Security.Tests.Builders
{
    public class SessionBuilder
    {
        DateTime _createdOn;
        DateTime _expiresOn;
        Guid _identifier;
        IUser _user;

        public SessionBuilder WithIdentifier(Guid value)
        {
            _identifier = value;

            return this;
        }

        public SessionBuilder WithUser(IUser value)
        {
            _user = value;

            return this;
        }

        public SessionBuilder WithCreatedOn(DateTime value)
        {
            _createdOn = value;

            return this;
        }

        public SessionBuilder WithExpiresOn(DateTime value)
        {
            _expiresOn = value;

            return this;
        }

        public ISession Build()
        {
            var session = Mock.Of<ISession>();

            session.Identifier = _identifier;
            session.User = _user;
            session.CreatedOn = _createdOn;
            session.ExpiresOn = _expiresOn;

            return session;
        }
    }
}