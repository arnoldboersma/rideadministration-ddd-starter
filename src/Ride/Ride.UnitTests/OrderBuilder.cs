using Ride.Domain.OrderAggregate;

namespace Ride.UnitTests;

public class OrderBuilder
{
    private readonly Order order;

    public OrderBuilder(Address address)
    {
        order = new Order(
            "description", 
            address
            );
    }

    public Order Build()
    {
        return order;
    }
}