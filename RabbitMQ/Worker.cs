using MassTransit;
using TrainingContract;

namespace RabbitMQ;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IBus            _bus;

    public Worker(ILogger<Worker> logger, IBus bus)
    {
        _logger = logger;
        _bus    = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Publish event PlaceOrderEvent at: {Time}", DateTimeOffset.Now);
            await _bus.Publish(new PlaceOrderEvent
                {
                    OrderNumber = $"Place order at: {DateTime.Now}"
                },
                stoppingToken);

            await Task.Delay(2000, stoppingToken);
            _logger.LogInformation("=========================================================");
        }
    }
}
