using MassTransit;
using TrainingContract;

namespace ScheduleMessage.Consumer.Consumers
{
    public class PlaceOrderDelayedConsumer : IConsumer<PlaceOrderEvent>
    {
        private readonly ILogger<PlaceOrderDelayedConsumer> _logger;

        public PlaceOrderDelayedConsumer(ILogger<PlaceOrderDelayedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<PlaceOrderEvent> context)
        {
            _logger.LogInformation("Consume from host {HostId} delayed {Delayed}",
                context.Message.HostId,
                DateTime.Now - context.Message.Timestamp);
            return Task.CompletedTask;
        }
    }
}
