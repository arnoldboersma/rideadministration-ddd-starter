using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideAdministration.Domain.RideAggregate;

namespace RideAdministration.Infrastructure.EntityConfigurations;

class RideEntityTypeConfiguration : IEntityTypeConfiguration<Ride>
{
    public void Configure(EntityTypeBuilder<Ride> rideConfiguration)
    {
        rideConfiguration.ToTable("ride", RideContext.DEFAULT_SCHEMA);
        rideConfiguration.HasKey(o => o.Id);
        rideConfiguration.Ignore(b => b.DomainEvents);
        rideConfiguration.Property(o => o.Id)
            .UseHiLo("rideseq", RideContext.DEFAULT_SCHEMA);

        rideConfiguration
            .Property<string>("Description")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Description")
            .IsRequired(false);
        rideConfiguration
            .Property<DateTime>("StartDate")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("StartDate")
            .IsRequired(true);

        var navigation = rideConfiguration.Metadata.FindNavigation(nameof(Ride.RideStops));
        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
