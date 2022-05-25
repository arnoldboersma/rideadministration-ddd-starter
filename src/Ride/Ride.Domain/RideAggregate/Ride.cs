using RideAdministration.Domain.Events;

namespace RideAdministration.Domain.RideAggregate
{
    public class Ride : Entity, IAggregateRoot 
    {
        public DateTime StartDate { get; private set; }
        public string Description { get; private set; } = string.Empty;
        private readonly List<RideStop> _rideStops;
        public IReadOnlyCollection<RideStop> RideStops => _rideStops;


        protected Ride() 
        {
            _rideStops = new List<RideStop>();
        }

        public Ride(DateTime startDate, string description) : this()
        {
            // TODO add validation
            StartDate = startDate;
            Description = description;

            // TODO Add Domain event
        }
    }
}
