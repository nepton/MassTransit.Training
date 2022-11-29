using MassTransit;

namespace Saga;

public class CreateBatteryEvent : CorrelatedBy<Guid>
{
    /// <summary>Returns the CorrelationId for the message</summary>
    public Guid CorrelationId { get; set; }
}
