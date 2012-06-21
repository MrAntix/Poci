using System.Collections.Generic;

namespace Poci.Contacts.Data
{
    public interface IContact
    {
        string Name { get; set; }
        ICollection<IEmail> Email { get; set; }
        ICollection<IPhone> Phone { get; set; }
        ICollection<IAddress> Addresses { get; set; }
    }
}