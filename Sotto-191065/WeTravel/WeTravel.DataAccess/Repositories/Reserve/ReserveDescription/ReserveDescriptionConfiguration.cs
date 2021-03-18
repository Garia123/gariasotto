using WeTravel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WeTravel.DataAccess
{
    public class ReserveDescriptionConfiguration : IEntityTypeConfiguration<ReserveDescription>
    {
        public void Configure(EntityTypeBuilder<ReserveDescription> builder)
        {
            builder.HasKey(r => r.ReserveId);
            builder.Property(r => r.State).IsRequired();
            builder.Property(r => r.Description).IsRequired();
        }
    }
}


