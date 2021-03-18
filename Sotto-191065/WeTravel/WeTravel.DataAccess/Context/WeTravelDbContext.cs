using WeTravel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using WeTravel.Domain.Entities;

namespace WeTravel.DataAccess
{
    [ExcludeFromCodeCoverageAttribute]
    public class WeTravelDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Domain.Lodging> Lodgings { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Domain.Reserve> Reserves { get; set; }
        public DbSet<ReserveDescription> ReserveDescriptions { get; set; }
        public DbSet<Domain.TouristLocation> TouristLocations { get; set; }
        public DbSet<TouristLocationCategory> TouristLocationCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Image> Images { get; set; }

        public WeTravelDbContext(DbContextOptions<WeTravelDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TouristLocationConfiguration());
            modelBuilder.ApplyConfiguration(new TouristLocationCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new RegionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new LodgingConfiguration());
            modelBuilder.ApplyConfiguration(new ReserveConfiguration());
            modelBuilder.ApplyConfiguration(new ReserveDescriptionConfiguration());
            modelBuilder.ApplyConfiguration(new SessionConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string directory = Directory.GetCurrentDirectory();

                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();

                var connectionString = configuration.GetConnectionString(@"sqlConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}

