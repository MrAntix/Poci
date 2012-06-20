using System;

namespace Poci.Security.Data
{
    public interface ISession
    {
        IUser User { get; set; }
        DateTime CreatedOn { get; }
    }
}