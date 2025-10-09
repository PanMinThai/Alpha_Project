using Shared.Base.Domain.DomainEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.Domain.Interface
{
    public interface IDomainEventEntity
    {
        IReadOnlyCollection<IEvent> DomainEvents { get; }
        void AddDomainEvent(IEvent domainEvent);
        void ClearDomainEvents();
    }
}
