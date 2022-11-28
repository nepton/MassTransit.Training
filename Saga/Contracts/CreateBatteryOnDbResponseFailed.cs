namespace Saga;

public class CreateBatteryOnDbResponseFailed
{
    public required string SerialNumber  { get; init; }
    public required Guid   BatteryCellId { get; init; }
}
