namespace Poci.Security.Data.Services
{
    public interface IUserDataService
    {
        IUser GetUserByEmail(
            string name);

        ISession CreateSession(
            IUser user);

        bool UserExistsByEmail(
            string email);

        IUser CreateUser(
            string name, string email, string passwordHash);
    }
}