namespace Outbox.Domain;

public class Order
{
    public Guid     Id           { get; set; }
    public string   CustomerName { get; set; }
    public string   ProductName  { get; set; }
    public int      Quantity     { get; set; }
    public decimal  Price        { get; set; }
    public decimal  TotalPrice   { get; set; }
    public DateTime CreatedAt    { get; set; }
}
