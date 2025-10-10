using Domain.DomainEvents;
using Domain.ValueObjects;
using Shared.Base.Domain.BaseClass;
using Shared.Base.Domain.DomainEvent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TaskEntity : SoftDeletableFullTrackedEntity
    {
        public string? Title { get; private set; }
        public DateTime? DueDate { get; private set; }
        public AppTaskStatus Status { get; private set; } = AppTaskStatus.InProgress;

        private TaskEntity() { } 

        public TaskEntity(string title, DateTime? dueDate)
        {
            Id = Guid.NewGuid();
            Title = title;
            DueDate = dueDate;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new TaskCreatedDomainEvent(Id, Title));
        }
        public void Update(string title, DateTime? dueDate, AppTaskStatus status)
        {
            Title = title;
            DueDate = dueDate;
            Status = status;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new TaskUpdatedDomainEvent(Id, Title, Status));
        }
        public void Delete()
        {
            AddDomainEvent(new TaskDeletedDomainEvent(Id));
        }
    }

}
