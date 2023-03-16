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

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {

            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");

            builder.Properties<TimeOnly>()
                .HaveConversion<TimeOnlyConverter>()
                .HaveColumnType("time");

            base.ConfigureConventions(builder);

        }

        public DbSet<Tape> Tapes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ActorProfile> ActorProfiles { get; set; }
        public DbSet<ReaderProfile> ReaderProfiles { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<PerfectSelfVersion> PerfectSelfVersions { get; set; }
    }
}
