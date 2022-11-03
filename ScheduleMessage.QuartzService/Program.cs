using MassTransit;
using Quartz;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(configureLogging => configureLogging.ClearProviders().AddSerilog())
    .ConfigureServices(services =>
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            q.UsePersistentStore(s =>
            {
                s.UseProperties = true;
                s.RetryInterval = TimeSpan.FromSeconds(15);

                s.UseSqlServer(options =>
                {
                    options.ConnectionString = "Server=.;Database=Quartz;Trusted_Connection=False;User Id=sa;Password=`1q;";
                    options.TablePrefix      = "QRTZ_";
                });

                s.UseJsonSerializer();
            });
        });

        services.AddMassTransit(configure =>
        {
            // 1. 处理 quartz 消息队列的消息，把他存到数据库 （发送消息端使用)
            // 2. 把到时间的消息从数据库中取出来，发送到消息队列
            configure.AddQuartzConsumers();

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
