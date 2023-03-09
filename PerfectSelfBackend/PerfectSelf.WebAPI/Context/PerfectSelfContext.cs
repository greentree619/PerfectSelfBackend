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

        public DbSet<Tape> Tapes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
