using CRUDAPPLICATION.Models.DEMON;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPPLICATION.DATA
{
    public class MVCDemoDbContext: DbContext
    {
        public MVCDemoDbContext(DbContextOptions options):base(options) { 
        
        }

        public DbSet<Employee> Employees { get; set; }

    }
}
