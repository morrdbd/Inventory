using System.Data.Entity;
using DOMoRR.Models.Tables;

namespace DOMoRR.Models
{
    public class DOMoRRDBContext : DbContext
    {
        public DbSet<ActivityLog> ActivityLogs { get; set; }

        public DbSet<LookupType> LookupTypes { get; set; }
        public DbSet<LookupValue> LookupValues { get; set; }

        public DbSet<SigninLog> SigninLogs { get; set; }

        public DbSet<RoleAccess> RoleAccess { get; set; }



        public DOMoRRDBContext() : base("InventoryCONN")
        {
            Configuration.ValidateOnSaveEnabled = false;
        }

        static DOMoRRDBContext()
        {
            Database.SetInitializer<DOMoRRDBContext>(null);
        }

        
    }
}