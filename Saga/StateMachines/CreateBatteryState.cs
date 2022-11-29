using MassTransit;

namespace Saga;

public class CreateBatteryState : SagaStateMachineInstance
{
    /// <summary>
    /// Identifies the saga instance uniquely, and is the primary correlation
    /// for the instance. While the setter is not typically called, it is there
    /// to support persistence consistently across implementations.
    /// </summary>
    public Guid CorrelationId { get; set; }

    public int CurrentState { get; set; }
    
    public Guid? SaveBatteryToTbRequestId { get; set; }
}
