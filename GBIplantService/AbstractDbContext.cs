using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GBIplantModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace GBIplantService
{
    [Table("AbstractDatabase")]
    public class AbstractDbContext : DbContext
    {
        public AbstractDbContext()
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual DbSet<Buyer> Buyers { get; set; }

        public virtual DbSet<GBIindgridient> GBIindgridients { get; set; }

        public virtual DbSet<Executor> Executors { get; set; }

        public virtual DbSet<Zakaz> Zakazes { get; set; }

        public virtual DbSet<GBIpieceOfArt> GBIpieceOfArts { get; set; }

        public virtual DbSet<GBIpieceofArt__ingridient> GBIpieceofArt__ingridients { get; set; }

        public virtual DbSet<Storage> Storages { get; set; }

        public virtual DbSet<Storage__GBIingridient> Storage__GBIingridients { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception)
            {
                foreach (var entry in ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.Reload();
                            break;
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                    }
                }
                throw;
            }
        }
    }
}
