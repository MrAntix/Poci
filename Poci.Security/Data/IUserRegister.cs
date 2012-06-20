namespace Poci.Security.Data
{
    public interface IUserRegister
    {
        string Name { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string PasswordConfirm { get;set;  }
    }
}