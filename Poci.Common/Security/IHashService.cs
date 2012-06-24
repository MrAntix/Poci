using System;

namespace Poci.Common.Security
{
    public interface IHashService :IDisposable
    {
        string Hash(string value);
        string Hash64(string value);
    }
}