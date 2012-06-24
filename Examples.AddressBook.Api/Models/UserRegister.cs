using Poci.Security.Data;

namespace Examples.AddressBook.Api.Models
{
    public class UserRegister : 
        IUserRegister
    {
        #region IUserRegister Members

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }

        #endregion
    }
}