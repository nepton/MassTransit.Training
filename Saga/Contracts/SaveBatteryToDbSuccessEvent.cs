using MassTransit;

namespace Saga;

public class SaveBatteryToDbSuccessEvent : CorrelatedBy<Guid>
{
    public Guid BatteryId { get; set; }

    /// <summary>Returns the CorrelationId for the message</summary>
    public Guid CorrelationId { get; set; }

    public required string OrderNumber { get; set; }
}
