using MassTransit;
using TrainingContract;

namespace Saga;

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
            // _publishEndpoint
            await _bus.Publish(new CreateBatteryEvent()
                {
                    CorrelationId = NewId.NextGuid(),
                },
                stoppingToken);

            _logger.LogInformation("Published CreateBatterySubmit");

            await Task.Delay(1000, stoppingToken);
        }
    }
}
