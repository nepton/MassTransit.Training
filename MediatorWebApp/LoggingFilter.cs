using MassTransit;

namespace MediatorWebApp;

public class LoggingFilter<T> : IFilter<SendContext<T>> where T : class
{
    private readonly IHttpContextAccessor _accessor;

    private readonly ILogger<LoggingFilter<T>> _logger;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public LoggingFilter(ILogger<LoggingFilter<T>> logger, IHttpContextAccessor accessor)
    {
        _logger   = logger;
        _accessor = accessor;
    }

    /// <summary>
    /// Sends a context to a filter, such that it can be processed and then passed to the
    /// specified output pipe for further processing.
    /// </summary>
    /// <param name="context">The pipe context type</param>
    /// <param name="next">The next pipe in the pipeline</param>
    /// <returns>An awaitable Task</returns>
    public Task Send(SendContext<T> context, IPipe<SendContext<T>> next)
    {
        var uow = _accessor.HttpContext?.RequestServices.GetRequiredService<IUnitOfWork>();
        _logger.LogInformation("Logging sending message unit of work is {UnitOfWork}", uow.InstanceId);

        return next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
        throw new NotImplementedException();
    }
}
