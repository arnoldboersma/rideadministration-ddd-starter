using Ride.Domain.OrderAggregate;

namespace Ride.UnitTests;
public class AddressBuilder
{
    public Address Build()
    {
        return new Address("street", "city", "state", "country", "zipcode");
    }
}
