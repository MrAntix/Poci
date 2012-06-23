using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Poci.Security.Data;
using Poci.Security.Properties;

namespace Poci.Security.Validation
{
    public class UserRegistrationValidator : IUserRegistrationValidator
    {
        #region IUserRegistrationValidator Members

        ValidationResult[] IUserRegistrationValidator.Validate(IUserRegister user)
        {
            return ValidateInternal(user).ToArray();
        }

        #endregion

        IEnumerable<ValidationResult> ValidateInternal(IUserRegister user)
        {
            if (user.Password == null
                || user.Password.Length < Settings.Default.UserPasswordMinLength)
            {
                yield return new ValidationResult(
                    string.Format(Settings.Default.UserPasswordMinLengthValidationMessage,
                                  Settings.Default.UserPasswordMinLength),
                    new[] {"Password"});
            }
        }
    }
}