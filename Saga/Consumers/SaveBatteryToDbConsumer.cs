using MassTransit;

namespace Saga;

public class SaveBatteryToDbConsumer : IConsumer<SaveBatteryToDbEvent>
{
    private readonly ILogger<SaveBatteryToDbConsumer> _logger;

    public SaveBatteryToDbConsumer(ILogger<SaveBatteryToDbConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<SaveBatteryToDbEvent> context)
    {
        _logger.LogInformation("Received CreateBatteryOnDbEvent {MessageCorrelateId} =====================", context.Message.CorrelationId);

        // 10% chance of failure
        if (new Random().Next(1, 10) == 1)
        {
            context.Publish(new SaveBatteryToDbFailureEvent()
            {
                CorrelationId = context.Message.CorrelationId,
                Message       = "Failed to save battery to db"
            });
            return Task.CompletedTask;
        }

        context.Publish(new SaveBatteryToDbSuccessEvent
        {
            BatteryId     = Guid.NewGuid(),
            CorrelationId = context.Message.CorrelationId
        });
        return Task.CompletedTask;
    }
}
