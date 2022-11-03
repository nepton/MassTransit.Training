using System.Reflection;
using MassTransit;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(configureLogging => configureLogging.ClearProviders().AddSerilog())
    .ConfigureServices(services =>
    {
        services.AddMassTransit(configure =>
        {
            configure.AddConsumers(Assembly.GetExecutingAssembly());

            configure.SetSnakeCaseEndpointNameFormatter();
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
        services.AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {
                options.WaitUntilStarted = true;
                options.StartTimeout     = TimeSpan.FromSeconds(10);
                options.StopTimeout      = TimeSpan.FromSeconds(10);
            });
    })
    .Build();

await host.RunAsync();
