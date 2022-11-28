namespace Saga;

public class CreateBatterySubmitEvent
{
    public required string SerialNumber { get; init; }

    public required Guid CreateBatteryId { get; init; }
}
