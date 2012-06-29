using Poci.Contacts.Data;

namespace Examples.AddressBook.EF.Data.Dtos
{
    public class AddressDto : DtoBase
    {
        public ContactDto Contact { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public Countries Country { get; set; }
    }
}