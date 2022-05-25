using Dapper;
using Microsoft.Data.SqlClient;

namespace RideAdministration.API.Application.Queries;

public class OrderQueries : IOrderQueries
{

    private string _connectionString = string.Empty;
    public OrderQueries(string constr)
    {
        _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
    }
    public async Task<IEnumerable<OrderSummary>> GetAllOrdersAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var result = await connection.QueryAsync<OrderSummary>(
            @"select	o.[Id] as ordernumber, 
			            o.Description as description,
                        o.Address_City as city,
                        o.RideStopCount as rideStopCount
                        FROM ride.Orders o"
            );

        if (result.AsList().Count == 0)
            throw new KeyNotFoundException();

        return result;
    }

    public async Task<Order> GetOrderAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var result = await connection.QueryAsync<dynamic>(
            @"select	o.[Id] as ordernumber, 
			            o.Description as description,
                        o.Address_City as city, 
			            o.Address_Country as country, 
			            o.Address_State as state, 
			            o.Address_Street as street, 
			            o.Address_ZipCode as zipcode,
                        o.RideStopCount as rideStopCount
                        FROM ride.Orders o
                        WHERE o.Id=@id"
                , new { id }
            );

        if (result.AsList().Count == 0)
            throw new KeyNotFoundException();

        return MapOrderItems(result);
    }

    private Order MapOrderItems(dynamic result)
    {
        var order = new Order
        {
            ordernumber = result[0].ordernumber,
            description = result[0].description,
            street = result[0].street,
            city = result[0].city,
            zipCode = result[0].zipcode,
            country = result[0].country,
            state = result[0].state,
            rideStopCount = result[0].rideStopCount
        };

        return order;
    }

}
