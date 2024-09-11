using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StoryTeller.Models;

namespace StoryTeller.AppDataContext
{
    public class StorytellerDbContext : DbContext
    {
        private readonly DbSettings _dbsettings;

        public StorytellerDbContext(IOptions<DbSettings> dbSettings)
        {
            _dbsettings = dbSettings.Value;
        }

        public DbSet<User> User { get; set; }
        public DbSet<ClassAbility> ClassAbility { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_dbsettings.ConnectionString);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("user").HasKey(x => x.Id);
            modelBuilder.Entity<ClassAbility>().ToTable("class_ability").HasKey(x => x.Id);
            modelBuilder.Entity<ClassAbility>().ToTable("class_ability").HasIndex(x => x.Id).IsUnique();

        }
    }
}

