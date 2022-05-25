using RideAdministration.API.Application.Commands;
using RideAdministration.API.Application.Queries;

namespace RideAdministration.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IOrderQueries _orderQueries;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(
    IMediator mediator,
    IOrderQueries orderQueries,
    ILogger<OrdersController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _orderQueries = orderQueries ?? throw new ArgumentNullException(nameof(orderQueries));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [Route("{orderId:int}")]
    [HttpGet]
    [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> GetOrderAsync(int orderId)
    {
        try
        {
            //Todo: It's good idea to take advantage of GetOrderByIdQuery
            //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
            var order = await _orderQueries.GetOrderAsync(orderId);

            return Ok(order);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderSummary>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<OrderSummary>>> GetOrdersAsync()
    {
        try
        {
            var orders = await _orderQueries.GetAllOrdersAsync();
            return Ok(orders);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<bool>> CreateOrderAsync([FromBody] CreateOrderCommand createOrderCommand)
    {
        _logger.LogInformation(
            "----- Sending command: {CommandName} - ({@Command})",
            "CreateOrderCommand",
            createOrderCommand);

        var id = await _mediator.Send(createOrderCommand);
        return Created($"api/orders/{id}", null);
    }

    [Route("setaddress")]
    [HttpPut]
    public async Task<ActionResult<bool>> SetAddressOrderAsync([FromBody] SetAddressOrderCommand setAddressOrderCommand)
    {
        _logger.LogInformation(
            "----- Sending command: {CommandName} - ({@Command})",
            "SetAddressOrderCommand",
            setAddressOrderCommand);

        var commandResult = await _mediator.Send(setAddressOrderCommand);
        if (!commandResult)
        {
            return BadRequest();
        }

        return Ok();
    }
}
