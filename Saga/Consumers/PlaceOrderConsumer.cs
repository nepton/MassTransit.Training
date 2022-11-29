using MassTransit;
using TrainingContract;

namespace Saga.Consumers;


public class PlaceOrderConsumer : IConsumer<PlaceOrderEvent>
{
    public Task Consume(ConsumeContext<PlaceOrderEvent> context)
    {
        Console.WriteLine("Order placed");
        return Task.CompletedTask;
    }
}
