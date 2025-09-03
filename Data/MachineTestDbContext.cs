using Microsoft.EntityFrameworkCore;
using MachineTest.Models;

namespace MachineTest.Data
{
    public class MachineTestDbContext : DbContext
    {
        public MachineTestDbContext(DbContextOptions<MachineTestDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}