namespace Poci.Security.Data
{
    public interface IUser
    {
        bool Active { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        string PasswordHash { get; set; }
    }
}