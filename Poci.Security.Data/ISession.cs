using System;

namespace Poci.Security.Data
{
    public interface ISession
    {
        Guid Identifier { get; set; }
        IUser User { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime ExpiresOn { get; set; }
    }
}