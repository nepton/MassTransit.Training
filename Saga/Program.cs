using System.Reflection;
using MassTransit;
using Saga;
using Serilog;

var host = Host.CreateDefaultBuilder(args);

host.ConfigureLogging(x =>
{
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .CreateLogger();

    x.ClearProviders();
    x.AddSerilog();
});

host.ConfigureServices(services =>
{
    services.AddHostedService<Worker>();

    services.AddMassTransit(x =>
    {
        x.AddSagaStateMachine<CreateBatteryStateMachine, CreateBatteryState>()
            .InMemoryRepository();
        x.AddConsumers(Assembly.GetExecutingAssembly());

        x.UsingInMemory((context, cfg) =>
        {
            cfg.ConfigureEndpoints(context);
        });
    });
});

await host.Build().RunAsync();
