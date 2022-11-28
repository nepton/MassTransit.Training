using MassTransit.Futures.Contracts;

namespace Saga;

public class UpdateCreatedBatteryRequest
{
    public required string SerialNumber    { get; init; }
    public required Guid   TbBatteryCellId { get; init; }
}
