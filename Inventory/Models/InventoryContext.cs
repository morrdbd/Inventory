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

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<TicketItem> TicketItem { get; set; }

        public DbSet<StockIn> StockIns { get; set; }

        public DbSet<StockInItem> StockInItems { get; set; }

        public DbSet<Branch> Branches { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<InHandStock> InHandStocks { get; set; }


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