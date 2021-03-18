using WeTravel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WeTravel.DataAccess
{
    public class TouristLocationCategoryConfiguration : IEntityTypeConfiguration<TouristLocationCategory>
    {   
        public void Configure(EntityTypeBuilder<TouristLocationCategory> builder)
        {
            builder.HasKey(tc => new { tc.TouristLocationId, tc.CategoryId });
            builder.HasOne(tc => tc.TouristLocation)
                .WithMany(t => t.TouristLocationCategories)
                .HasForeignKey(tc => tc.TouristLocationId);
            builder.HasOne(tc => tc.Category)
                .WithMany(c => c.TouristLocationCategories)
                .HasForeignKey(tc => tc.CategoryId);
        }
    }
}


