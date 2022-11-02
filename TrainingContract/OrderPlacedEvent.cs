using System;

namespace TrainingContract;

public record OrderPlacedEvent
{
    public Guid OrderId { get; set; }

    public string? OrderNumber { get; set; }
}



// {
//  "messageId": "7c740000-a5be-c025-8e9a-08dabc788911",
//  "requestId": null,
//  "correlationId": null,
//  "conversationId": "7c740000-a5be-c025-8c47-08dabc7888fc",
//  "initiatorId": null,
//  "sourceAddress": "rabbitmq://localhost/PlaceOrder",
//  "destinationAddress": "rabbitmq://localhost/TrainingContract:OrderPlacedEvent",
//  "responseAddress": null,
//  "faultAddress": null,
//  "messageType": [
//    "urn:message:TrainingContract:OrderPlacedEvent"
//  ],
//  "message": {
//    "orderId": "064bf0c3-e095-4518-a86d-1c8194efb792",
//    "orderNumber": "Place order at: 11/2/2022 10:18:30 AM"
//  },
//  "expirationTime": null,
//  "sentTime": "2022-11-02T02:18:30.8073114Z",
//  "headers": {},
//  "host": {
//    "machineName": "NEPTON-WORK",
//    "processName": "RabbitMQ",
//    "processId": 29820,
//    "assembly": "RabbitMQ",
//    "assemblyVersion": "1.0.0.0",
//    "frameworkVersion": "6.0.10",
//    "massTransitVersion": "8.0.7.0",
//    "operatingSystemVersion": "Microsoft Windows NT 10.0.22621.0"
//  }
//}
