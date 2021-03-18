using WeTravel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WeTravel.DataAccess
{
    public class ReserveConfiguration : IEntityTypeConfiguration<Domain.Reserve>
    {
        public void Configure(EntityTypeBuilder<Domain.Reserve> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.CheckIn).IsRequired();
            builder.Property(r => r.CheckOut).IsRequired();
            builder.Property(r => r.Adults).IsRequired();
            builder.Property(r => r.Price).IsRequired();
            builder.Property(r => r.Telephone).IsRequired();
            builder.Property(r => r.ContactFirstName).IsRequired();
            builder.Property(r => r.ContactLastName).IsRequired();
            builder.Property(r => r.ContactEmail).IsRequired();
            builder.HasOne(r => r.Lodging).WithMany(l => l.Reserves).HasForeignKey(r => r.LodgingId);
        }
    }
}


