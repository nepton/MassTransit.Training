namespace MediatorWebApp;

public interface IUnitOfWork
{
    Guid InstanceId { get; }
}
