using System;

namespace TrainingContract;

public record OrderPlacedEvent
{
    public Guid OrderId { get; set; }

    public string? OrderNumber { get; set; }
}
