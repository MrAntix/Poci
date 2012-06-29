using System.Data.Entity;

namespace Examples.AddressBook.EF.Data.Dtos
{
    internal class DtoContext : DbContext
    {
        public DbSet<ContactDto> Contacts { get; set; }
        public DbSet<SessionDto> Sessions { get; set; }
        public DbSet<UserDto> Users { get; set; }
    }

}