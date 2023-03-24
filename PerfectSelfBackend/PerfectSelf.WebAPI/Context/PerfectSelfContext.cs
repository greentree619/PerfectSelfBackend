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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<ReaderList>();
            modelBuilder.Ignore<BookList>();
            modelBuilder
               .Entity<ReaderList>()
               .ToView("readerlist")
               .HasKey(t => t.Uid);
            modelBuilder
               .Entity<BookList>()
               .ToView("booklist")
               .HasKey(t => t.Id);
        }

        public DbSet<Tape> Tapes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ActorProfile> ActorProfiles { get; set; }
        public DbSet<ReaderProfile> ReaderProfiles { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<PerfectSelfVersion> PerfectSelfVersions { get; set; }
        public DbSet<ReaderList> ReaderLists { get; set; }
        public DbSet<BookList> BookLists { get; set; }
    }
}
