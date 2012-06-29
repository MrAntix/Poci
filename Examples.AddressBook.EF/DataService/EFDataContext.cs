using System.Data.Entity;
using System.Threading.Tasks;
using Examples.AddressBook.EF.Data;
using Poci.Common.DataServices;

namespace Examples.AddressBook.EF.DataService
{
    public sealed class EFDataContext : DbContext, IDataContext
    {
        public DbSet<EFContact> Contacts { get; set; }
        public DbSet<EFSession> Sessions { get; set; }
        public DbSet<EFUser> Users { get; set; }

        #region IDataContext Members

        public async Task CommitAsync()
        {
            SaveChanges();
        }

        #endregion
    }
}