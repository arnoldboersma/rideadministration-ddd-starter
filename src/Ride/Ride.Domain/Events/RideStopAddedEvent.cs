using MediatR;
using RideAdministration.Domain.RideAggregate;

namespace RideAdministration.Domain.Events;

public class RideStopAddedEvent : INotification
{
    public RideStop RideStop { get; }
    public RideStopAddedEvent(RideStop rideStop)
    {
        RideStop = rideStop;
    }
}
