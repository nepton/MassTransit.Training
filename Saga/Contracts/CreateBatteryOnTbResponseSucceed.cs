namespace Saga;

public class CreateBatteryOnTbResponseSucceed
{
    public required string SerialNumber    { get; init; }
    public required Guid   TbBatteryCellId { get; init; }
}
