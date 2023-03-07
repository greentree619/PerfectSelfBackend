namespace PerfectSelf.WebAPI.Context
{
    using Microsoft.EntityFrameworkCore;
    using PerfectSelf.WebAPI.Models;

    public class PerfectSelfContext
        : DbContext
    {
        public PerfectSelfContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
