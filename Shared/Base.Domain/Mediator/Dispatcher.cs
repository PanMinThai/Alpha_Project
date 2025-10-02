using Microsoft.Extensions.DependencyInjection;
using Shared.Base.Domain.DomainEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.Domain.Mediator
{
    public class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider _provider;

        public Dispatcher(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResponse));
            var handler = _provider.GetRequiredService(handlerType);
            return await (Task<TResponse>)handlerType
                .GetMethod("Handle")!
                .Invoke(handler, new object[] { command, cancellationToken })!;
        }

        public async Task<TResponse> Query<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse));
            var handler = _provider.GetRequiredService(handlerType);
            return await (Task<TResponse>)handlerType
                .GetMethod("Handle")!
                .Invoke(handler, new object[] { query, cancellationToken })!;
        }

        public async Task Publish(IEvent @event, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());
            var handlers = _provider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                await (Task)handlerType
                    .GetMethod("Handle")!
                    .Invoke(handler, new object[] { @event, cancellationToken })!;
            }
        }
    }


}
