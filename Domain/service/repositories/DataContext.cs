using core.domain;
using Microsoft.EntityFrameworkCore;


namespace core.service.repositories
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Transfer> transfers { get; set; }
    }

}
