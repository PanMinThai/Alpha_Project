using Shared.Base.Domain.DomainEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.Domain.Mediator
{
    public interface IDispatcher
    {
        Task<TReply> Send<TReply>(ICommand<TReply> command, CancellationToken cancellationToken = default);
        Task Trigger(IEvent @event, CancellationToken cancellationToken = default);
    }
}
