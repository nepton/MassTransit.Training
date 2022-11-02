using System.Reflection;
using MassTransit;
using RabbitMQ;
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
            });
        });

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
