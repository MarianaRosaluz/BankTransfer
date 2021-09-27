using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Domain.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Transfer> transfers { get; set; }
    }

}
