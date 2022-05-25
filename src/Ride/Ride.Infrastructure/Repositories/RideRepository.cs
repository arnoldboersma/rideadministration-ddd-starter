using RideAdministration.Domain.RideAggregate;

namespace RideAdministration.Infrastructure.Repositories;

public class RideRepository : IRideRepository
{
    private readonly RideContext _context;
    public IUnitOfWork UnitOfWork => _context;
    public RideRepository(RideContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Ride Add(Ride ride)
    {
        return _context.Rides.Add(ride).Entity;
    }

    public async Task<Ride> GetAsync(int rideId)
    {
        var ride = await _context
                            .Rides
                            .FirstOrDefaultAsync(o => o.Id == rideId);
        if (ride == null)
        {
            ride = _context
                        .Rides
                        .Local
                        .FirstOrDefault(o => o.Id == rideId);
        }

        if (ride != null)
        {
            await _context.Entry(ride)
                .Collection(i => i.RideStops).LoadAsync();
        }

        return ride;
    }
}
