using MassTransit;

namespace Saga;

public class SaveBatteryToDbFailureEvent : CorrelatedBy<Guid>
{
    /// <summary>Returns the CorrelationId for the message</summary>
    public Guid CorrelationId { get; set; }

    public string Message { get; set; }
}
