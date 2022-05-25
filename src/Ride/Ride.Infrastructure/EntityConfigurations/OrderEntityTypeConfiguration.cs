using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideAdministration.Domain.OrderAggregate;

namespace RideAdministration.Infrastructure.EntityConfigurations;

class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> orderConfiguration)
    {
        orderConfiguration.ToTable("orders", RideContext.DEFAULT_SCHEMA);
        orderConfiguration.HasKey(o => o.Id);
        orderConfiguration.Ignore(b => b.DomainEvents);
        orderConfiguration.Ignore(b => b.Address);
        orderConfiguration.Property(o => o.Id)
            .UseHiLo("orderseq", RideContext.DEFAULT_SCHEMA);

        //Address value object persisted as owned entity in EF Core 2.0
        orderConfiguration.OwnsOne(o => o.Address);
        orderConfiguration.Property<string>("Description").IsRequired(false);
        orderConfiguration.Property<int>("RideStopCount").IsRequired(true);
    }
}
