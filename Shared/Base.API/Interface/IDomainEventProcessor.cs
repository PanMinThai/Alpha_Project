using Shared.Base.Domain.DomainEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.API.Interface
{
    public interface IDomainEventProcessor
    {
        Task TriggerExecutingDomainEvents(IEnumerable<IEvent> domainEvents, CancellationToken cancellationToken = default);
        Task TriggerExecutedDomainEvents(IEnumerable<IEvent> domainEvents, CancellationToken cancellationToken = default);
    }
}
