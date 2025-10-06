using Shared.Base.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shared.Base.Domain.BaseClass
{
    public class IntegrationEventEntity : CreationTrackedEntity
    {
        internal IntegrationEventEntity() { }

        //public IntegrationEventEntity(IIntegrationEvent integrationEvent, Guid transactionId)
        //{
        //    ArgumentNullException.ThrowIfNull(integrationEvent);
        //    Id = Guid.NewGuid();
        //    BusinessId = integrationEvent.Id;
        //    EventType = integrationEvent.GetType().FullName!;
        //    Payload = JsonSerializer.Serialize(integrationEvent, integrationEvent.GetType());
        //    TransactionId = transactionId;
        //    Status = IntegrationEventStatusEnum.Pending;
        //}

        //public Guid BusinessId { get; private set; }
        //public string EventType { get; private set; }
        //public string Payload { get; private set; }
        //public Guid TransactionId { get; private set; }
        //public DateTimeOffset? ProcessAt { get; private set; }
        //public IntegrationEventStatusEnum Status { get; private set; }

        //public void MarkAsProcessed()
        //{
        //    Status = IntegrationEventStatusEnum.Processed;
        //    ProcessAt = DateTimeOffset.UtcNow;
        //}

        //public void MarkAsFailed()
        //{
        //    Status = IntegrationEventStatusEnum.Failed;
        //    ProcessAt = DateTimeOffset.UtcNow;
        //}
    }
}
