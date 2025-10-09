using Microsoft.Extensions.Logging;
using Shared.Base.API.Helper;
using Shared.Base.API.Interface;
using Shared.Base.Domain.DomainEvent;
using Shared.Base.Domain.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.API.BaseClass
{
    public class DomainEventProcessor : IDomainEventProcessor
    {
        private readonly IDispatcher _dispatcher;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DomainEventProcessor> _logger;

        public DomainEventProcessor(
            IDispatcher dispatcher,
            IServiceProvider serviceProvider,
            ILogger<DomainEventProcessor> logger)
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            _serviceProvider = serviceProvider;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task TriggerExecutingDomainEvents(IEnumerable<IEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in domainEvents)
            {
                Console.WriteLine($"🟢 [Executing] Processing domain event: {domainEvent.GetType().Name}");
                _logger.LogInformation("Executing domain event: {DomainEventName}", domainEvent.GetType().Name);

                await _dispatcher.Trigger(domainEvent, cancellationToken);
            }
        }

        public async Task TriggerExecutedDomainEvents(IEnumerable<IEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in domainEvents)
            {
                Console.WriteLine($"🟢 [Executed] Processing domain event: {domainEvent.GetType().Name}");
                _logger.LogInformation("Executed domain event: {DomainEventName}", domainEvent.GetType().Name);

                var handlerType = typeof(IEnumerable<>)
                    .MakeGenericType(typeof(IExecutedDomainEventHandler<>)
                        .MakeGenericType(domainEvent.GetType()));

                var handlers = _serviceProvider.GetService(handlerType) as IEnumerable<object> ?? Array.Empty<object>();
                var handlerArray = handlers.ToArray();

                var eventName = domainEvent.GetType().Name;

                _logger.LogInformation(
                    "Handling executed domain event {DomainEventName} with {HandlerCount} handlers.",
                    eventName,
                    handlerArray.Length);

                foreach (var handlerObj in handlerArray)
                {
                    try
                    {
                        var handler = (dynamic)handlerObj;
                        await handler.Handle((dynamic)domainEvent, cancellationToken);

                        Console.WriteLine($"✅ [Handled] {handlerObj.GetType().Name} handled {eventName}");
                    }
                    catch (Exception ex)
                    {
                        var handlerName = handlerObj.GetType().Name;
                        var serializedEvent = SerializerHelper.SerializeByJsonConvert(domainEvent);

                        _logger.LogError(
                            ex,
                            "Error handling domain event {DomainEventName} with handler {HandlerName}. Event data: {DomainEvent}.",
                            eventName,
                            handlerName,
                            serializedEvent);
                    }
                }
            }
        }
    }


}
