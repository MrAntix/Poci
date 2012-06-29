using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examples.AddressBook.EF.Data.Dtos
{
    public class ContactDto : DtoBase
    {
        public string Name { get; set; }
        public string InformalName { get; set; }
        public string Notes { get; set; }

        [InverseProperty("Contact")]
        public Collection<EmailDto> Emails { get; set; }

        [InverseProperty("Contact")]
        public Collection<PhoneDto> Phones { get; set; }

        [InverseProperty("Contact")]
        public Collection<AddressDto> Addresses { get; set; }

        public UserDto Owner { get; set; }
    }
}