using System.Reflection;
using MassTransit;
using MassTransit.Monitoring.Performance;
using RabbitMQ;
using RabbitMQ.Consumers;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(configureLogging => configureLogging.ClearProviders().AddSerilog())
    .ConfigureServices(services =>
    {
        services.AddMassTransit(configure =>
        {
            configure.AddConsumers(Assembly.GetExecutingAssembly());
            configure.AddConsumer<RabbitMQ.Consumers.OrderPlacedConsumer>()
                .Endpoint(e =>
                {
                    e.Name = "order-service";   // specify the queue name
                });
            configure.AddConsumer<RabbitMQ.NewConsumers.OrderPlacedConsumer>()
                .Endpoint(e =>
                {
                    e.Name = "order-service";   // specify the queue name
                });

            configure.SetKebabCaseEndpointNameFormatter();
            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost",
                    "/",
                    h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                cfg.ConfigureEndpoints(context);
                
                // explicitly declare the queue consumer by the queue name, PrefetchCount etc.
                // cfg.ReceiveEndpoint("order-service", e =>
                // {
                //     e.ConfigureConsumer<PlaceOrderConsumer>(context);
                // });
            });
        });
        services.AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {
                options.WaitUntilStarted = true;
                options.StartTimeout     = TimeSpan.FromSeconds(10);
                options.StopTimeout      = TimeSpan.FromSeconds(10);
            });

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
