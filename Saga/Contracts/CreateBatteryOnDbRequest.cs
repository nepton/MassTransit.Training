namespace Saga;

public class CreateBatteryOnDbRequest
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public CreateBatteryOnDbRequest(string serialNumber)
    {
        SerialNumber = serialNumber;
    }

    public string SerialNumber { get; }
}
