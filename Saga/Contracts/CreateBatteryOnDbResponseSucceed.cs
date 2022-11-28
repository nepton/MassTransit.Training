namespace Saga;

public class CreateBatteryOnDbResponseSucceed
{
    public required string SerialNumber  { get; init; }
    public required Guid   BatteryCellId { get; init; }
}
