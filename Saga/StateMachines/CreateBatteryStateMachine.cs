using MassTransit;

namespace Saga;

public class CreateBatteryStateMachine : MassTransitStateMachine<CreateBatteryState>
{
    public CreateBatteryStateMachine()
    {
        InstanceState(x => x.CurrentState, DbCreating, TbCreating, DbUpdating, DbRolling, Done);

        // Event
        Event(() => CreateBatterySubmit, x => x.CorrelateById(e => e.Message.CreateBatteryId));

        // Behavior
        Initially(
            When(CreateBatterySubmit)
                .Publish(context => new CreateBatteryOnDbRequest(context.Message.SerialNumber))
                .TransitionTo(DbCreating)
        );

        During(DbCreating,
            When(CreateBatteryOnDbResponseSucceed)
                .Publish(context => new CreateBatteryOnTbRequest
                {
                    SerialNumber = context.Message.SerialNumber,
                })
                .TransitionTo(TbCreating),
            When(CreateBatteryOnDbResponseFailed)
                .TransitionTo(Done)
                .Finalize());

        During(TbCreating,
            When(CreateBatteryOnTbResponseSucceed)
                .Publish(context => new UpdateCreatedBatteryRequest()
                {
                    SerialNumber    = context.Message.SerialNumber,
                    TbBatteryCellId = context.Message.TbBatteryCellId,
                })
                .TransitionTo(DbUpdating),
            When(CreateBatteryOnTbResponseFailed)
                .Publish(context => new RollbackCreatedBatteryRequest
                {
                    SerialNumber = context.Message.SerialNumber,
                })
                .TransitionTo(Done)
                .Finalize());

        SetCompletedWhenFinalized();
    }

    public State? DbCreating { get; set; }
    public State? TbCreating { get; set; }
    public State? DbUpdating { get; set; }
    public State? DbRolling  { get; set; }
    public State? Done       { get; set; }

    public Event<CreateBatterySubmitEvent>? CreateBatterySubmit { get; set; }

    public Event<CreateBatteryOnDbResponseSucceed>? CreateBatteryOnDbResponseSucceed { get; set; }

    public Event<CreateBatteryOnDbResponseFailed>? CreateBatteryOnDbResponseFailed { get; set; }

    public Event<CreateBatteryOnTbResponseSucceed> CreateBatteryOnTbResponseSucceed { get; set; }
    public Event<CreateBatteryOnTbResponseFailed>  CreateBatteryOnTbResponseFailed  { get; set; }
}
