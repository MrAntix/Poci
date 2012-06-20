namespace Poci.Common.Security
{
    public interface IHashService
    {
        string Hash(string value);
        string Hash64(string value);
    }
}