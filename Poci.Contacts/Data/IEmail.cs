namespace Poci.Contacts.Data
{
    public interface IEmail
    {
        string Type { get; set; }
        string Address { get; set; }
    }
}