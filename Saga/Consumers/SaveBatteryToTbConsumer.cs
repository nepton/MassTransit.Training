using MassTransit;

namespace Saga;

public class SaveBatteryToTbConsumer : IConsumer<SaveBatteryToTb>
{
    private readonly ILogger<SaveBatteryToTbConsumer> _logger;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public SaveBatteryToTbConsumer(ILogger<SaveBatteryToTbConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<SaveBatteryToTb> context)
    {
        // we have 20% chance to fail
        // if (new Random().Next(1, 100) < 20)
        // {
        //     _logger.LogError("Failed to save battery to TB");
        //     throw new Exception("Failed to save battery to TB");
        // }

        _logger.LogInformation("Saved battery to TB");
        await context.RespondAsync(new SaveBatteryToTbResponse
        {
            OrderNumber = context.Message.OrderNumber,
            TbBatteryId = Guid.NewGuid(),
        });
    }
}
