using Domain.ValueObjects;
using Shared.Base.Domain;
using Shared.Base.Domain.DomainEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainEvents
{
    public class TaskDomainEvent { }
    public class TaskCreatedDomainEvent : IEvent
    {
        public Guid TaskId { get; }
        public string Title { get; }
        public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;

        public TaskCreatedDomainEvent(Guid taskId, string title)
        {
            TaskId = taskId;
            Title = title;
        }
    }
    public class TaskUpdatedDomainEvent : IEvent
    {
        public Guid TaskId { get; }
        public string Title { get; }
        public AppTaskStatus Status { get; }
        public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;

        public TaskUpdatedDomainEvent(Guid taskId, string title, AppTaskStatus status)
        {
            TaskId = taskId;
            Title = title;
            Status = status;
        }
    }
    public class TaskDeletedDomainEvent : IEvent
    {
        public Guid TaskId { get; }
        public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;

        public TaskDeletedDomainEvent(Guid taskId)
        {
            TaskId = taskId;
        }
    }
}
