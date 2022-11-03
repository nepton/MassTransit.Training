using MassTransit;
using Outbox.Domain;
using TrainingContract;

namespace Outbox;

public class Worker : BackgroundService
{
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
        var       dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
        // var       publisher = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
        var scheduler = scope.ServiceProvider.GetRequiredService<IMessageScheduler>();

        await using var transaction = await dbContext.Database.BeginTransactionAsync(stoppingToken);

        // add order
        var order = new Order
        {
            Id           = Guid.NewGuid(),
            CustomerName = "Nepton",
            ProductName  = "Dishwasher",
            Quantity     = 0,
            Price        = 0,
            TotalPrice   = 0,
            CreatedAt    = DateTime.Now,
        };
        await dbContext.Orders.AddAsync(order, stoppingToken);
        await dbContext.SaveChangesAsync(stoppingToken);
        _logger.LogInformation("Order added, order id: {OrderId}", order.Id);

        // publish order created event
        await scheduler.SchedulePublish(
            DateTime.Now.AddSeconds(20),
            new OrderPlacedEvent
            {
                OrderId = order.Id,
            },
            stoppingToken);
        await dbContext.SaveChangesAsync(stoppingToken);
        _logger.LogInformation("Order created event published, order id = {OrderId}", order.Id);

        // AFTER SAVE CHANGES, THE OUTBOX PROCESSOR WILL SEND THE MESSAGES
        await transaction.CommitAsync(stoppingToken);
        _logger.LogInformation("Commit changes to DB");
    }
}
