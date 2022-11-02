using MassTransit;

namespace MediatorWebApp.Commands;

public class CreateOrderCommandConsumer : IConsumer<CreateOrderCommand>
{
    private readonly ILogger<CreateOrderCommandConsumer> _logger;
    private readonly IUnitOfWork                         _unitOfWork;
    private readonly IPlaceOrderService                  _placeOrderService;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public CreateOrderCommandConsumer(ILogger<CreateOrderCommandConsumer> logger, IUnitOfWork unitOfWork, IPlaceOrderService placeOrderService)
    {
        _logger            = logger;
        _unitOfWork        = unitOfWork;
        _placeOrderService = placeOrderService;
    }

    public async Task Consume(ConsumeContext<CreateOrderCommand> context)
    {
        _logger.LogInformation("CreateOrderCommandConsumer: {ProductName}, Unit of work is {UnitOfWork}", context.Message.ProductName, _unitOfWork.InstanceId);
        await _placeOrderService.PlaceOrderAsync();
        await context.RespondAsync(new CreateOrderCommandResponse(new Random().Next()));
    }
}
