// See https://aka.ms/new-console-template for more information

using MassTransit;
using TrainingContract;

var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host("localhost",
        h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
});

await busControl.StartAsync();
while (true)
{
    Console.Write("Enter the order number: ");
    var value = Console.ReadLine();

    if (!string.IsNullOrEmpty(value))
    {
        await busControl.Publish(new PlaceOrderEvent
        {
            OrderNumber = value
        });
        Console.WriteLine("Order placed");
    }
}
