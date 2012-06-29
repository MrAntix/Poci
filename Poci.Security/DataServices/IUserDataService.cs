using Poci.Security.Data;

namespace Poci.Security.DataServices
{
    public interface IUserDataService
    {
        IUser TryGetUser(
            string email);

        bool UserExists(
            string email);

        IUser CreateUser(
            string name, string email, string passwordHash);

        void InsertUser(
            IUser user);

        void UpdateUser(
            IUser user);
    }
}