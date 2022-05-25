namespace RideAdministration.API.Application.Queries;

public record Order
{
    public int ordernumber { get; init; }
    public string description { get; init; } = string.Empty;
    public string street { get; init; } = string.Empty;
    public string city { get; init; } = string.Empty;
    public string state { get; init; } = string.Empty;
    public string country { get; init; } = string.Empty;
    public string zipCode { get; init; } = string.Empty;
    public int rideStopCount { get; init; } = 0;
}

public record OrderSummary
{
    public int ordernumber { get; init; }
    public string description { get; init; } = string.Empty;
    public string city { get; init; } = string.Empty;
    public int rideStopCount { get; init; } = 0;
}

