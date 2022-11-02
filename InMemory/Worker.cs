using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using TrainingContract;

namespace InMemory;

public class Worker : BackgroundService
{
    readonly IBus _bus;

    public Worker(IBus bus)
    {
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _bus.Publish(new PlaceOrderEvent() {OrderNumber = $"Create order at {DateTimeOffset.Now}"}, stoppingToken);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
