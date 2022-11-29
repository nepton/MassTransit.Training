namespace Saga;

public class SaveBatteryToTbResponse
{
    public required string OrderNumber { get; set; }
    public required Guid TbBatteryId   { get; set; }
}
