using MassTransit;
using TrainingContract;

namespace ScheduleMessage.Producer;

public class Worker : BackgroundService
{
    private readonly Guid             _id = Guid.NewGuid();
    private readonly ILogger<Worker>  _logger;
    private readonly IServiceProvider _serviceProvider;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger          = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope     = _serviceProvider.CreateScope();
        var       scheduler = scope.ServiceProvider.GetRequiredService<IMessageScheduler>();

        var delayInSec = 10;

        while (!stoppingToken.IsCancellationRequested)
        {
            await scheduler.SchedulePublish(DateTime.Now.AddSeconds(delayInSec),
                new PlaceOrderEvent
                {
                    Timestamp   = DateTime.Now,
                    OrderNumber = $"Delay is {delayInSec} sec",
                    HostId      = _id,
                },
                stoppingToken);

            await Task.Delay(1000, stoppingToken);
            _logger.LogInformation("Publish from host {HostId} delay is {DelayInSec} sec", _id, delayInSec);
        }
    }
}
