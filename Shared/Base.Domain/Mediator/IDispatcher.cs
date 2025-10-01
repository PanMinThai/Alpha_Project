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
        Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);
        Task<TResponse> Query<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);
        Task Trigger(IEvent @event, CancellationToken cancellationToken = default);
    }
}
