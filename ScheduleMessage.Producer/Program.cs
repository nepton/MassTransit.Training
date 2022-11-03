using MassTransit;
using ScheduleMessage.Producer;
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
            // 支持延迟发布消息
            configure.AddPublishMessageScheduler();

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

                // 通知 rabbitmq 把延迟消息发送到 MassTransit.Scheduling:ScheduleMessage 交换机
                cfg.UsePublishMessageScheduler();
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
