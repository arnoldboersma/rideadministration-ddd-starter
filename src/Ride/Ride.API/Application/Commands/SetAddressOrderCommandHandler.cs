using RideAdministration.Domain.OrderAggregate;

namespace RideAdministration.API.Application.Commands;

public class SetAddressOrderCommandHandler : IRequestHandler<SetAddressOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<SetAddressOrderCommandHandler> _logger;

    public SetAddressOrderCommandHandler(IOrderRepository orderRepository, ILogger<SetAddressOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(SetAddressOrderCommand command, CancellationToken cancellationToken)
    {
        var orderToUpdate = await _orderRepository.GetAsync(command.OrderId);
        if (orderToUpdate is null)
        {
            return false;
        }
        var address = new Address(command.Street, command.City, command.State, command.Country, command.ZipCode);
        orderToUpdate.SetAddress(address);
        _logger.LogInformation("----- Set Address Order - Order: {@Order}", orderToUpdate);
        
        
        return await _orderRepository.UnitOfWork
            .SaveEntitiesAsync(cancellationToken);
    }
}
