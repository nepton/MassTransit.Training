using MassTransit;
using TrainingContract;

namespace Saga;

public class CreateBatteryStateMachine : MassTransitStateMachine<CreateBatteryState>
{
    public CreateBatteryStateMachine(ILogger<CreateBatteryStateMachine> logger)
    {
        InstanceState(x => x.CurrentState, Done);

        // Event
        Event(() => PlaceOrder, x => x.CorrelateById(e => e.Message.OrderId));

        // Behavior
        Initially(
            When(PlaceOrder)
                .Then(x => logger.LogInformation("Received PlaceOrder ====================="))
                .TransitionTo(Done)
        );

        SetCompletedWhenFinalized();
    }

    public State? Done { get; set; }

    public Event<PlaceOrderEvent>? PlaceOrder { get; set; }
}
