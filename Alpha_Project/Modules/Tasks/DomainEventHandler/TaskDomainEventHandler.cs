using Domain.DomainEvents;
using Shared.Base.Domain.Mediator;
using Shared.Base.Infra.Interfaces;

namespace Alpha_Project.Modules.Tasks.DomainEventHandler
{
    public class TaskDomainEventHandler :
    IEventHandler<TaskCreatedDomainEvent>,
    IEventHandler<TaskUpdatedDomainEvent>,
    IEventHandler<TaskDeletedDomainEvent>
    {
        private readonly ILogService _logService;
        private readonly IEmailService _emailService;

        public TaskDomainEventHandler(ILogService logService, IEmailService emailService)
        {
            _logService = logService;
            _emailService = emailService;
        }

        public async Task Handle(TaskCreatedDomainEvent @event, CancellationToken cancellationToken)
        {
            await _logService.LogAsync($"Task Created: {@event.Title} ({@event.TaskId})");

            await _emailService.SendEmailAsync(
                "titus@company.com",
                "Task Created",
                $"A new task '{@event.Title}' was created at {@event.OccurredOn}"
            );
        }

        public async Task Handle(TaskUpdatedDomainEvent @event, CancellationToken cancellationToken)
        {
            await _logService.LogAsync($"Task Updated: {@event.Title} ({@event.TaskId}) - Status: {@event.Status}");

            await _emailService.SendEmailAsync(
                "titus@company.com",
                "Task Updated",
                $"Task '{@event.Title}' was updated to status '{@event.Status}' at {@event.OccurredOn}"
            );
        }

        public async Task Handle(TaskDeletedDomainEvent @event, CancellationToken cancellationToken)
        {
            await _logService.LogAsync($"Task Deleted: {@event.TaskId}");

            await _emailService.SendEmailAsync(
                "titus@company.com",
                "Task Deleted",
                $"Task '{@event.TaskId}' was deleted at {@event.OccurredOn}"
            );
        }
    }

}
