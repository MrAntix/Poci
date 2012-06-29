using System.ComponentModel.DataAnnotations;

namespace Examples.AddressBook.EF.Data.Dtos
{
    public class DtoBase
    {
        [Key]
        public int Id { get; set; }
    }
}