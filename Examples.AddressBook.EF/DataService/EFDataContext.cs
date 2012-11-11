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

#pragma warning disable 1998
        public async Task CommitAsync()
#pragma warning restore 1998
        {
            SaveChanges();
        }

        #endregion
    }
}