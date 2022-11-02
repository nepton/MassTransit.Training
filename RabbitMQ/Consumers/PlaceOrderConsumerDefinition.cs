// using MassTransit;
//
// namespace RabbitMQ.Consumers
// {
//     public class PlaceOrderConsumerDefinition :
//         ConsumerDefinition<PlaceOrderConsumer>
//     {
//         protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<PlaceOrderConsumer> consumerConfigurator)
//         {
//             endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
//         }
//     }
// }

