using Poci.Security.Data;

namespace Examples.AddressBook.Api.Models
{
    public class UserLogOn:
        IUserLogOn
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}