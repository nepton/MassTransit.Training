using MassTransit;

namespace Saga;

public class UpdateCreatedBatteryConsumer : IConsumer<UpdateCreatedBatteryRequest>
{
    private readonly ILogger<UpdateCreatedBatteryConsumer> _logger;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public UpdateCreatedBatteryConsumer(ILogger<UpdateCreatedBatteryConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<UpdateCreatedBatteryRequest> context)
    {
        _logger.LogInformation("CreateBatteryOnTbRequest");

        // create new battery cell, so we will get the id of the new battery cell
        var tbBatteryCellId = Guid.NewGuid();

        return context.Publish(new UpdateCreatedBatteryResponse()
        {
            SerialNumber = context.Message.SerialNumber,
        });
    }
}
