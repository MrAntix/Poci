namespace Poci.Security.Data
{
    public interface IUserLogOn
    {
        string Email { get; set; }
        string Password { get; set; }
    }
}