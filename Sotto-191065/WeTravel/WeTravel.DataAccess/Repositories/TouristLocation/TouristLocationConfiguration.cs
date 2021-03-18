using WeTravel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WeTravel.DataAccess
{
    public class TouristLocationConfiguration : IEntityTypeConfiguration<Domain.TouristLocation>
    {
        public void Configure(EntityTypeBuilder<Domain.TouristLocation> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired();
        }
    }
}


