using System;

namespace TrainingContract
{
    public record PlaceOrderEvent
    {
        public string?  OrderNumber { get; set; }
        public DateTime Timestamp   { get; set; }
        public Guid     HostId      { get; set; }
    }
}
