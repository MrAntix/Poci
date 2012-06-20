using System;
using Moq;
using Poci.Security.Data;

namespace Poci.Security.Tests.Builders
{
    class SessionBuilder
    {
        IUser _user;
        DateTime _createdOn;

        public SessionBuilder WithUser(IUser user)
        {
            _user = user;

            return this;
        }

        public SessionBuilder WithCreatedOn(DateTime createdOn)
        {
            _createdOn = createdOn;

            return this;
        }

        public ISession Build()
        {
            var mock = new Mock<ISession>();
            mock.SetupAllProperties();

            mock
                .Setup(s => s.User)
                .Returns(_user);
            mock
                .Setup(s => s.CreatedOn)
                .Returns(_createdOn);

            return mock.Object;
        }
    }
}