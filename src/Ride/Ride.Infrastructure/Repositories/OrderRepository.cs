using RideAdministration.Domain.OrderAggregate;

namespace RideAdministration.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly RideContext _context;
    public IUnitOfWork UnitOfWork => _context;
    public OrderRepository(RideContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Order Add(Order order)
    {
        return _context.Orders.Add(order).Entity;
    }

    public async Task<Order> GetAsync(int orderId)
    {
        var order = await _context
                            .Orders
                            .Include(x => x.Address)
                            .FirstOrDefaultAsync(o => o.Id == orderId);
        if (order == null)
        {
            order = _context
                        .Orders
                        .Local
                        .FirstOrDefault(o => o.Id == orderId);
        }

        return order;
    }

    public void Update(Order order)
    {
        _context.Entry(order).State = EntityState.Modified;
    }
}
