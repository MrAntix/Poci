using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examples.AddressBook.EF.Data.Dtos
{
    public class SessionDto {

        [Key]
        public Guid Identifier { get; set; }
        public UserDto User { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}