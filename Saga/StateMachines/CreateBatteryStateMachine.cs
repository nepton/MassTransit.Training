using MassTransit;

namespace Saga;

public class CreateBatteryStateMachine : MassTransitStateMachine<CreateBatteryState>
{
    public CreateBatteryStateMachine(ILogger<CreateBatteryStateMachine> logger)
    {
        InstanceState(x => x.CurrentState, DbCreating, Done);

        Event(() => SaveBatteryToDbSuccess,
            // The correlationId is not found in the state machine, so it is not possible to correlate the event to the state machine instance
            e => e.OnMissingInstance(m => m.Execute(x => logger.LogError("Cannot find instance for {CorrelationId}", x.Message.CorrelationId))));

        // Behavior
        Initially(
            When(CreateBattery)
                .Then(x => logger.LogInformation("Received CreateBattery Submit {MessageCorrelateId} =====================", x.Message.CorrelationId))
                .Publish(context => new SaveBatteryToDbEvent
                {
                    CorrelationId = context.Message.CorrelationId,
                })
                .TransitionTo(DbCreating)
        );

        During(DbCreating,
            When(SaveBatteryToDbSuccess)
                .Then(x => logger.LogInformation("Received CreateBatteryOnDbSuccessEvent {MessageCorrelateId} =====================", x.Message.CorrelationId))
                .TransitionTo(Done)
                .Finalize(),
            When(SaveBatteryToDbFailure)
                .Then(x => logger.LogWarning("Received CreateBatteryOnDbFailedEvent {MessageCorrelateId} {Message} =====================",
                    x.Message.CorrelationId,
                    x.Message.Message))
                .TransitionTo(Done)
                .Finalize()
        );

        SetCompletedWhenFinalized();
    }

    public State? DbCreating { get; set; }
    public State? Done       { get; set; }

    public Event<CreateBatteryEvent>?          CreateBattery          { get; set; }
    public Event<SaveBatteryToDbSuccessEvent>? SaveBatteryToDbSuccess { get; set; }

    public Event<SaveBatteryToDbFailureEvent>? SaveBatteryToDbFailure { get; set; }
}
