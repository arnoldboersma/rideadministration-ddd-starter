namespace RideAdministration.Domain.RideAggregate
{
    public class RideStop : Entity
    {
        private bool _delivered = false;

        public int OrderId { get; private set; }
        public Address? Address { get; private set; }

        protected RideStop() {}
        public RideStop(int orderId, Address? address)
        {
            OrderId = orderId;
            Address = address;
            // TODO Trigger domain event
        }
    }
}
