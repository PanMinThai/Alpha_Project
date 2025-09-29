using Domain.DomainEvents;
using Domain.ValueObjects;
using Shared.Base.Domain;
using Shared.Base.Domain.DomainEvent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TaskEntity : IdEntity<Guid>
{
    public Guid CreatedBy { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Title { get; set; }
    public DateTime? DueDate { get; set; }
    public AppTaskStatus Status { get; set; } = AppTaskStatus.InProgress;

    public void Created(string title, Guid createdBy, DateTime? dueDate = null)
    {
        Id = Guid.NewGuid();
        Title = title;
        CreatedBy = createdBy;
        CreatedAt = DateTime.UtcNow;
        DueDate = dueDate;

        AddDomainEvent(new TaskCreatedDomainEvent
        {
            TaskId = Id,
            Title = Title,
            CreatedBy = CreatedBy,
            DueDate = DueDate,
            Status = Status
        });
    }

    public void Update(string title, DateTime? dueDate)
    {
        Title = title;
        DueDate = dueDate;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new TaskUpdatedDomainEvent
        {
            TaskId = Id,
            Title = Title,
            DueDate = DueDate,
            Status = Status
        });
    }

    public void Complete()
    {
        Status = AppTaskStatus.Completed;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new TaskCompletedDomainEvent { TaskId = Id });
    }
}
}
