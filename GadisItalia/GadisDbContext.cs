using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadisItalia
{
    class GadisDbContext : DbContext
    {
        public GadisDbContext() : base("name=GadisDatabaseConnection") {}
        public DbSet<Supplier> Suppliers { get; set; }
    }
}
