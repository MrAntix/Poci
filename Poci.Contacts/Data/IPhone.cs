namespace Poci.Contacts.Data
{
    public interface IPhone
    {
        string Type { get; set; }
        Countries Country { get; set; }
        string Number { get; set; }
        string Extension { get; set; }
    }
}