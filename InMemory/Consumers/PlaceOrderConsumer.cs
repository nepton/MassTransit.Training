using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using TrainingContract;

namespace InMemory.Consumers
{
    public class PlaceOrderConsumer : IConsumer<PlaceOrderEvent>
    {
        private readonly ILogger<PlaceOrderConsumer> _logger;

        public PlaceOrderConsumer(ILogger<PlaceOrderConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<PlaceOrderEvent> context)
        {
            _logger.LogInformation("Received message: {Message}", context.Message);
            context.RespondAsync(new OrderPlacedEvent()
            {
                OrderId     = Guid.NewGuid(),
                OrderNumber = context.Message.OrderNumber,
            });

            return Task.CompletedTask;
        }
    }
}
