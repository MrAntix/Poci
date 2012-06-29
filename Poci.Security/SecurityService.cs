using System;
using System.Linq;
using Poci.Common.Security;
using Poci.Common.Validation;
using Poci.Security.Data;
using Poci.Security.DataServices;
using Poci.Security.Properties;
using Poci.Security.Validation;

namespace Poci.Security
{
    public sealed class SecurityService : ISecurityService
    {
        readonly IHashService _hashService;
        readonly ISessionDataService _sessionDataService;
        readonly IUserDataService _userDataService;
        readonly IUserRegistrationValidator _userRegistrationValidator;

        public SecurityService(
            IUserDataService userDataService,
            ISessionDataService sessionDataService,
            IHashService hashService,
            IUserRegistrationValidator userRegistrationValidator)
        {
            _userDataService = userDataService;
            _sessionDataService = sessionDataService;

            _hashService = hashService;
            _userRegistrationValidator = userRegistrationValidator;
        }

        #region ISecurityService Members

        ISession ISecurityService.LogOn(IUserLogOn credentials)
        {
            var user = _userDataService.TryGetUser(credentials.Email);
            if (user != null
                && user.Active
                && user.PasswordHash == _hashService.Hash64(credentials.Password))
            {
                return CreateInsertSession(user);
            }

            return null;
        }

        ISession ISecurityService.Register(IUserRegister details)
        {
            if (!_userDataService.UserExists(details.Email))
            {
                // validation
                RegistrationValidate(details);

                var user = _userDataService.CreateUser(
                    details.Name, details.Email,
                    _hashService.Hash64(details.Password));

                user.Active = true;

                _userDataService.InsertUser(user);

                return CreateInsertSession(user);
            }

            return null;
        }

        public bool SessionIsValid(ISession session)
        {
            return session != null
                   && _sessionDataService
                          .SessionExists(session.Identifier, session.User, false);
        }

        public void AssertSessionIsValid(ISession session)
        {
            if (!SessionIsValid(session))
                throw new InvalidSessionException();
        }

        #endregion

        ISession CreateInsertSession(IUser user)
        {
            var session = _sessionDataService.CreateSession(user);

            session.Identifier = Guid.NewGuid();
            session.CreatedOn = DateTime.UtcNow;
            session.ExpiresOn = DateTime.UtcNow
                .AddMinutes(Settings.Default.SessionExpiryMinutes);

            _sessionDataService.InsertSession(session);

            return session;
        }

        void RegistrationValidate(
            IUserRegister details)
        {
            var errors = _userRegistrationValidator.Validate(details);

            if (errors.Any())
                throw new ValidationResultsException(
                    "Registration failed",
                    errors);
        }
    }
}