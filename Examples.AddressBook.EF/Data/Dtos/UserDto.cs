namespace Examples.AddressBook.EF.Data.Dtos
{
    public class UserDto : DtoBase
    {
        public bool Active { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}