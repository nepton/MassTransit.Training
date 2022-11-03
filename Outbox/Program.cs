using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Outbox;
using Serilog;
using Serilog.Events;

// Outbox project used to demonstrate the use of the transactional outbox pattern
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console()
    .CreateLogger();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging((context, logging) =>
    {
        logging.ClearProviders();
        logging.AddSerilog();
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddMassTransit(options =>
        {
            options.AddConsumers(Assembly.GetExecutingAssembly());
            options.AddPublishMessageScheduler();

            options.UsingRabbitMq((context, configure) =>
            {
                configure.ConfigureEndpoints(context);
                configure.UsePublishMessageScheduler();
            });

            // Entity Framework Core based outbox
            options.AddEntityFrameworkOutbox<OrderDbContext>(o =>
            {
                o.UseBusOutbox();
                o.UseSqlServer();
                o.QueryDelay = TimeSpan.FromSeconds(2);
            });
        });

        services.AddDbContext<OrderDbContext>(options =>
        {
            options.UseSqlServer("Server=(local);Database=MassTransitOutbox;Trusted_Connection=False;User Id=sa;Password=`1q;");
        });
    })
    .Build();

// prepare the database
using (var scope = host.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    dbContext.Database.EnsureCreated();
}

await host.RunAsync();
