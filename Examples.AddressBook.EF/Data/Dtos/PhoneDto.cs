using Poci.Contacts.Data;

namespace Examples.AddressBook.EF.Data.Dtos
{
    public class PhoneDto : DtoBase
    {
        public ContactDto Contact { get; set; }
        public string Type { get; set; }
        public Countries Country { get; set; }
        public string Number { get; set; }
        public string Extension { get; set; }
    }
}