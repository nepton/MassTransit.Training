using MassTransit;
using TrainingContract;

namespace RabbitMQ.NewConsumers;

/// <summary>
/// 他与 Consumers/OrderPlacedConsumer 共用交换机和队列
/// </summary>
public class OrderPlacedConsumer : IConsumer<OrderPlacedEvent>
{
    private readonly ILogger<OrderPlacedConsumer> _logger;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public OrderPlacedConsumer(ILogger<OrderPlacedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<OrderPlacedEvent> context)
    {
        _logger.LogInformation("New OrderPlacedConsumer: {OrderId}", context.Message.OrderId);
        return Task.CompletedTask;
    }
}
