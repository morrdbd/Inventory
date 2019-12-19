namespace DOMoRR.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class tempModel : DbContext
    {
        public tempModel()
            : base("name=tempModel")
        {
        }

        public virtual DbSet<DistributionTicketPC5> DistributionTicketPC5 { get; set; }
        public virtual DbSet<ReceiptReportMem7> ReceiptReportMem7 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
