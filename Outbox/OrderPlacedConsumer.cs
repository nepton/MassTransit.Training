using MassTransit;
using TrainingContract;

namespace Outbox;

public class OrderPlacedConsumer : IConsumer<OrderPlacedEvent>
{
    private readonly ILogger<OrderPlacedConsumer> _logger;

    public OrderPlacedConsumer(ILogger<OrderPlacedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<OrderPlacedEvent> context)
    {
        _logger.LogInformation("OrderPlacedEvent received. order id = {OrderId}", context.Message.OrderId);
        return Task.CompletedTask;
    }
}
