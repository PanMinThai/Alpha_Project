using Domain.ValueObjects;
using Shared.Base.Domain.Mediator;
using System.Windows.Input;

namespace Alpha_Project.Modules.Tasks.Command
{
    public class TaskCommand{}
    public class CreateTaskCommand : ICommand<Guid>
    {
        public string Title { get; set; }
        public AppTaskStatus Status { get; set; } = AppTaskStatus.InProgress; 
        public DateTime DueDate { get; set; }
    }
    public class UpdateTaskCommand : ICommand<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public AppTaskStatus Status { get; set; }
        public DateTime DueDate { get; set; }
    }
    public class DeleteTaskCommand : ICommand<Guid>    
    {
        public Guid Id { get; set; }
    }
}
