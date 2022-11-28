using MassTransit;

namespace Saga;

public class CreateBatteryOnTbConsumer : IConsumer<CreateBatteryOnTbRequest>
{
    private readonly ILogger<CreateBatteryOnTbConsumer> _logger;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public CreateBatteryOnTbConsumer(ILogger<CreateBatteryOnTbConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<CreateBatteryOnTbRequest> context)
    {
        _logger.LogInformation("CreateBatteryOnTbRequest");

        // create new battery cell, so we will get the id of the new battery cell
        var tbBatteryCellId = Guid.NewGuid();

        return context.Publish(new CreateBatteryOnTbResponseSucceed()
        {
            SerialNumber    = context.Message.SerialNumber,
            TbBatteryCellId = tbBatteryCellId,
        });
    }
}
