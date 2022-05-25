using Ride.Domain.OrderAggregate;

namespace Ride.UnitTests.Domain;

public class OrderAggregateTest
{
    [Fact]
    public void Add_new_Order_raises_new_event()
    {
        //Arrange
        var street = "fakeStreet";
        var city = "FakeCity";
        var state = "fakeState";
        var country = "fakeCountry";
        var zipcode = "FakeZipCode";

        var expectedResult = 1;

        //Act 
        var fakeOrder = new Order("fakeName", new Address(street, city, state, country, zipcode));

        //Assert
        Assert.Equal(fakeOrder.DomainEvents.Count, expectedResult);
    }
}

