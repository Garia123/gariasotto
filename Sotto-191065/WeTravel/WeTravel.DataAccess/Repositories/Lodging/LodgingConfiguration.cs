using WeTravel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace WeTravel.DataAccess
{
    public class LodgingConfiguration : IEntityTypeConfiguration<Domain.Lodging>
    {
        public void Configure(EntityTypeBuilder<Domain.Lodging> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Name).IsRequired();
            builder.Property(l => l.Address).IsRequired();
            builder.Property(l => l.Stars).IsRequired();
            builder.Property(l => l.PricePerNight).IsRequired();
            builder.Property(l => l.Available).IsRequired();
        }
    }
}


