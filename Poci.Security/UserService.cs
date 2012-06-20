using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Poci.Common.Security;
using Poci.Common.Validation;
using Poci.Security.Data;
using Poci.Security.Data.Services;
using Poci.Security.Validation;

namespace Poci.Security
{
    public class UserService : IUserService
    {
        readonly IUserDataService _dataService;
        readonly IUserRegistrationValidator _userRegistrationValidator;

        readonly IHashService _hashService;

        public UserService(
            IUserDataService dataService,
            IHashService hashService, 
            IUserRegistrationValidator userRegistrationValidator)
        {
            _dataService = dataService;
            _hashService = hashService;
            _userRegistrationValidator = userRegistrationValidator;
        }

        #region IUserService Members

        ISession IUserService.LogOn(IUserLogOn credentials)
        {
            var user = _dataService.GetUserByEmail(credentials.Email);
            if (user.Active
                && user.PasswordHash == _hashService.Hash64(credentials.Password))
            {
                var session = _dataService.CreateSession(user);

                return session;
            }

            return null;
        }

        ISession IUserService.Register(IUserRegister details)
        {
            if (!_dataService.UserExistsByEmail(details.Email))
            {
                // validation
                RegistrationValidate(details);

                var user = _dataService.CreateUser(
                    details.Name, details.Email,
                    _hashService.Hash64(details.Password));

                var session = _dataService.CreateSession(user);

                return session;
            }

            return null;
        }

        void IDisposable.Dispose()
        {
        }

        #endregion

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