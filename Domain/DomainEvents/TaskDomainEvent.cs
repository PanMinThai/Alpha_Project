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
        public required Guid TaskId { get; init; }
        public required string Title { get; init; }
        public required Guid CreatedBy { get; init; }
        public DateTime? DueDate { get; init; }
        public AppTaskStatus Status { get; init; }
        public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
    }

    public class TaskUpdatedDomainEvent : IEvent
    {
        public required Guid TaskId { get; init; }
        public required string Title { get; init; }
        public DateTime? DueDate { get; init; }
        public AppTaskStatus Status { get; init; }
        public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
    }

    public class TaskCompletedDomainEvent : IEvent
    {
        public required Guid TaskId { get; init; }
        public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
    }


}
