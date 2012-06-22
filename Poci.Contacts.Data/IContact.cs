using System.Collections.Generic;

namespace Poci.Contacts.Data
{
    public interface IContact
    {
        string Name { get; set; }
        string InformalName { get; set; }
        string Notes { get; set; }
        ICollection<IEmail> Emails { get; set; }
        ICollection<IPhone> Phones { get; set; }
        ICollection<IAddress> Addresses { get; set; }
    }
}