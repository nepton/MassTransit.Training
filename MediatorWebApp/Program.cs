using System.Reflection;
using MassTransit;
using MediatorWebApp;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {sourceContext} {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IPlaceOrderService, PlaceOrderService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddMediator(cfg =>
{
    cfg.AddConsumers(Assembly.GetExecutingAssembly());
    cfg.ConfigureMediator((context, mediatorCfg) =>
    {
        // use the http controller scope
        mediatorCfg.UseHttpContextScopeFilter(context);
        mediatorCfg.UseSendFilter(typeof(LoggingFilter<>), context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();