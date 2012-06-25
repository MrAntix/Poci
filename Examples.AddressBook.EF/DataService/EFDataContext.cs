using System;
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

        async Task IDataContext.CommitAsync()
        {
            SaveChanges();
        }

        #endregion

        #region IDisposable

        bool _disposed;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // Dispose managed resources.
            }

            // unmanaged resources here.

            _disposed = true;
        }

        #endregion
    }
}