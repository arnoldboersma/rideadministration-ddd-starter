using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideAdministration.Domain.OrderAggregate;
using RideAdministration.Domain.RideAggregate;

namespace RideAdministration.Infrastructure.EntityConfigurations;

class RideStopEntityTypeConfiguration : IEntityTypeConfiguration<RideStop>
{
    public void Configure(EntityTypeBuilder<RideStop> rideStopConfiguration)
    {
        rideStopConfiguration.ToTable("ridestop", RideContext.DEFAULT_SCHEMA);
        rideStopConfiguration.HasKey(o => o.Id);
        rideStopConfiguration.Ignore(b => b.DomainEvents);
        rideStopConfiguration.Property(o => o.Id)
            .UseHiLo("ridestopseq", RideContext.DEFAULT_SCHEMA);

        rideStopConfiguration.Property<int>("RideId")
            .IsRequired();

        rideStopConfiguration
            .HasOne<Order>()
            .WithMany()
            .HasForeignKey("OrderId")
            .IsRequired(true);
        
        rideStopConfiguration.Property<int>("OrderId")
            .IsRequired();

        //Address value object persisted as owned entity in EF Core 2.0
        rideStopConfiguration.OwnsOne(o => o.Address);
        rideStopConfiguration
            .Property<bool>("_delivered")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Delivered")
            .IsRequired(true);
    }
}
