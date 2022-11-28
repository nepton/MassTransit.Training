using MassTransit;

namespace Saga;

public class CreateBatteryOnDbConsumer : IConsumer<CreateBatteryOnDbRequest>
{
    private readonly ILogger<CreateBatteryOnDbConsumer> _logger;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public CreateBatteryOnDbConsumer(ILogger<CreateBatteryOnDbConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<CreateBatteryOnDbRequest> context)
    {
        _logger.LogInformation("CreateBatteryOnDbConsumer");

        // create new battery cell, so we will get the id of the new battery cell
        var batteryCellId = Guid.NewGuid();

        return context.Publish(new CreateBatteryOnDbResponseSucceed()
        {
            SerialNumber  = context.Message.SerialNumber,
            BatteryCellId = batteryCellId,
        });
    }
}
