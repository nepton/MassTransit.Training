using MassTransit;

namespace RabbitMQ.Consumers
{
    public class PlaceOrderConsumerDefinition : ConsumerDefinition<PlaceOrderConsumer>
    {
        public PlaceOrderConsumerDefinition()
        {
            EndpointName           = "order-service"; // change the name of endpoint MUST place in the ctor
            ConcurrentMessageLimit = 10;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<PlaceOrderConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}
