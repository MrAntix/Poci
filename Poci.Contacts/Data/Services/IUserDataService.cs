namespace Poci.Contacts.Data.Services
{
    public interface IContactDataService
    {
        IContact CreateContact();
        IContact UpdateContact();
    }
}