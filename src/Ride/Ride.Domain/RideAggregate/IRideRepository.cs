namespace RideAdministration.Domain.RideAggregate;

public interface IRideRepository : IRepository<Ride>
{
    Ride Add(Ride ride);
    Task<Ride> GetAsync(int rideId);
}
