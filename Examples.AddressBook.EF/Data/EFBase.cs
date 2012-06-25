using System.ComponentModel.DataAnnotations;

namespace Examples.AddressBook.EF.Data
{
    public abstract class EFBase<TI>
        where TI : class
    {
        [Key]
        public int Id { get; set; }

        protected TI This
        {
            get { return this as TI; }
        }
    }
}