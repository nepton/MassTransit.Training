namespace MediatorWebApp.Commands;

public class CreateOrderCommandResponse
{
    public CreateOrderCommandResponse(int orderId)
    {
        OrderId = orderId;
    }

    public int OrderId { get; }
}
