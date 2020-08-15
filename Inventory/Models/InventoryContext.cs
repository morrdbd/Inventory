using System.Data.Entity;
using Inventory.Models.Tables;
using Inventory.Models.ViewModels;

namespace Inventory.Models
{
    public class InventoryDBContext : DbContext
    {
        public DbSet<ActivityLog> ActivityLogs { get; set; }

        public DbSet<LookupType> LookupTypes { get; set; }

        public DbSet<LookupValue> LookupValues { get; set; }

        public DbSet<SigninLog> SigninLogs { get; set; }

        public DbSet<RoleAccess> RoleAccess { get; set; }

        public DbSet<Distribution> Distributions { get; set; }

        public DbSet<DistributionItem> DistributionItems { get; set; }

        public DbSet<Consume> Consumes { get; set; }

        public DbSet<ConsumeItem> ConsumesItems { get; set; }

        public DbSet<StockIn> StockIns { get; set; }

        public DbSet<StockInItem> StockInItems { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Restore> Restores { get; set; }

        public DbSet<RestoreItem> RestoreItems { get; set; }

        public InventoryDBContext() : base("InventoryCONN")
        {
            Configuration.ValidateOnSaveEnabled = false;
        }

        static InventoryDBContext()
        {
            Database.SetInitializer<InventoryDBContext>(null);
        }

    }
}