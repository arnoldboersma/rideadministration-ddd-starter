namespace RideAdministration.API.Application.Commands;


[DataContract]
public class SetAddressOrderCommand
    : IRequest<bool>
{
    public int OrderId { get; private set; }
    public string Street { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string State { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;
    public string ZipCode { get; private set; } = string.Empty;

    public SetAddressOrderCommand(int orderId, string street, string city, string state, string country, string zipCode)
    {
        OrderId = orderId;
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }
    
}