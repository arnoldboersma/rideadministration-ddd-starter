namespace RideAdministration.API.Application.Queries
{
    public interface IOrderQueries
    {
        Task<Order> GetOrderAsync(int id);
        Task<IEnumerable<OrderSummary>> GetAllOrdersAsync();
    }
}
