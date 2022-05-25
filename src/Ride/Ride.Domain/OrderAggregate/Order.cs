namespace RideAdministration.Domain.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public string Description { get; private set; } = string.Empty;
        public Address? Address { get; private set; }
        public int RideStopCount { get; private set; } = 0;


        protected Order() { }

        public Order(string description, Address? address = null) : this()
        {
            Description = description;
            Address = address;
        }
        public void SetAddress(Address address)
        {
            Address = address;
        }

        public void AddRideStop()
        {
            RideStopCount++;
        }
    }
}
