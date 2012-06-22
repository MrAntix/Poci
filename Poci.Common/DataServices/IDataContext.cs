using System;
using System.Threading.Tasks;

namespace Poci.Common.DataServices
{
    public interface IDataContext :
        IDisposable
    {
        Task CommitAsync();
    }
}