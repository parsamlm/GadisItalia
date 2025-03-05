using System.Data.Entity;

namespace GadisItalia.Models
{
    class GadisDbContext : DbContext
    {
        public GadisDbContext() : base("name=GadisDatabaseConnection") {}
        public DbSet<Supplier> Suppliers { get; set; }
    }
}
