using Shared.Base.Domain.DomainEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.Domain.Mediator
{
    public interface IExecutedDomainEventHandler<in T> where T : IEvent
    {
        Task Handle(T @event, CancellationToken token);
    }
}
