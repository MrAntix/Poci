using System.ComponentModel.DataAnnotations;
using Poci.Security.Data;

namespace Poci.Security.Validation
{
    public interface IUserRegistrationValidator
    {
        ValidationResult[] Validate(IUserRegister user);
    }
}