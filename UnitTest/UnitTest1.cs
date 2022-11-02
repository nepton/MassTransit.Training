using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Consumers;
using TrainingContract;

namespace UnitTest;

public class PlaceOrderConsumerTester
{
    [Fact]
    public async Task TestTheEventHasBeenConsumed()
    {
        await using var provider = new ServiceCollection()
            .AddMassTransitTestHarness(x =>
            {
                x.AddConsumer<PlaceOrderConsumer>();
            })
            .BuildServiceProvider(true);

        var harness = provider.GetTestHarness();
        await harness.Start();
        
        var bus     = harness.Bus;
        await bus.Publish(new PlaceOrderEvent
        {
            OrderNumber = "123"
        });

        Assert.True(await harness.Consumed.Any<PlaceOrderEvent>());

        var consumer = harness.GetConsumerHarness<PlaceOrderConsumer>();
        Assert.True(await consumer.Consumed.Any<PlaceOrderEvent>());
    }
}
