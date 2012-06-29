namespace Examples.AddressBook.EF.Data.Dtos
{
    public class EmailDto : DtoBase
    {
        public ContactDto Contact { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
    }
}