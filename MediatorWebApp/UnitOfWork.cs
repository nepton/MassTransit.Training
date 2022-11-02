namespace MediatorWebApp;

public class UnitOfWork : IUnitOfWork
{
    private readonly Guid _instanceId = Guid.NewGuid();

    public Guid InstanceId => _instanceId;
}
