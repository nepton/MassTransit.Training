namespace MediatorWebApp;

public interface IPlaceOrderService
{
    Task PlaceOrderAsync();
}

class PlaceOrderService : IPlaceOrderService
{
    private readonly IUnitOfWork                _unitOfWork;
    private readonly ILogger<PlaceOrderService> _logger;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public PlaceOrderService(IUnitOfWork unitOfWork, ILogger<PlaceOrderService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger     = logger;
    }

    public Task PlaceOrderAsync()
    {
        _logger.LogInformation("Placing order..... Unit of work is {UnitOfWork}", _unitOfWork.InstanceId);
        return Task.CompletedTask;
    }
}
