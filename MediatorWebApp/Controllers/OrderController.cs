using MassTransit.Mediator;
using MediatorWebApp.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MediatorWebApp.Controllers;

/// <summary>
/// Create place order controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator                _mediator;
    private readonly IUnitOfWork              _unitOfWork;
    private readonly IPlaceOrderService       _placeOrderService;
    private readonly ILogger<OrderController> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    public OrderController(IMediator mediator, IPlaceOrderService placeOrderService, IUnitOfWork unitOfWork, ILogger<OrderController> logger)
    {
        _mediator          = mediator;
        _placeOrderService = placeOrderService;
        _unitOfWork        = unitOfWork;
        _logger            = logger;
    }

    /// <summary>
    /// Place order
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PlaceOrderAsync([FromBody] CreateOrderCommand request)
    {
        _logger.LogInformation("Web request received, Unit of work Id: {UnitOfWorkId}", _unitOfWork.InstanceId);
        await _placeOrderService.PlaceOrderAsync();

        var client   = _mediator.CreateRequestClient<CreateOrderCommand>();
        var response = await client.GetResponse<CreateOrderCommandResponse>(request);

        return Ok(response);
    }
}
